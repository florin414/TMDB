using System.Security.Cryptography;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PopcornHub.Application.IServices;
using PopcornHub.Domain.Entities;
using PopcornHub.Domain.IRepositories;
using PopcornHub.Domain.Models.Auth;
using PopcornHub.Shared.Utils;

namespace PopcornHub.Application.Services;

public class AuthService : IAuthService
{
    private readonly IRepositoryProvider _repositoryProvider;
    private readonly ITokenService _tokenService;
    private readonly ILogger<AuthService> _logger;
    private readonly IAuthenticationCookieHelper _cookieHelper;

    public AuthService(ITokenService tokenService, IRepositoryProvider repositoryProvider, ILogger<AuthService> logger, IAuthenticationCookieHelper cookieHelper)
    {
        _tokenService = tokenService;
        _repositoryProvider = repositoryProvider;
        _logger = logger;
        _cookieHelper = cookieHelper;
    }

    public async Task<Result> RegisterAsync(RegisterModel model)
    {
        try
        {
            var repo = _repositoryProvider.GetRepository();
            var query = await repo.GetQueryableAsync<User>();
        
            if (await query.AnyAsync(u => u.UserName == model.UserName || u.Email == model.Email))
                return Result.Failure("Username or email already exists");

            var salt = RandomNumberGenerator.GetBytes(16);
            var hash = PasswordHelper.HashPassword(model.Password, salt);
            var user = new User(model.UserName, model.Email, Convert.ToBase64String(hash), Convert.ToBase64String(salt), PasswordHelper.Cost);
       
            await repo.InsertAsync(user);
            await repo.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception e)
        {
            _logger.LogError("An error occurred while registering the user. Message={Message}", e.Message);
            return Result.Failure("An error occurred while registering the user.");
        }
    }

    public async Task<Result> LoginAsync(LoginModel model)
    {
        try
        {
            var repo = _repositoryProvider.GetReadOnlyRepository();
            var query = await repo.GetQueryableAsync<User>();

            var userResult = await query.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (userResult == null)
                return Result.Failure<string>("Invalid username or password");

            var passwordCheck = PasswordHelper.VerifyPassword(model.Password, userResult.PasswordHash, userResult.PasswordSalt, userResult.BcryptCost);
            if (!passwordCheck)
                return Result.Failure<string>("Invalid username or password");

            var tokenResult = _tokenService.GenerateToken(userResult);
        
            if (tokenResult.IsFailure)
                return Result.Failure<string>("Error generating token");
            
            _cookieHelper.SetCookie(tokenResult.Value);

            return Result.Success();
        }
        catch (Exception e)
        {
            _logger.LogError("An error occurred during login. Message={Message}", e.Message);
            return Result.Failure<string>("An error occurred during login.");
        }
    }
}

