using CSharpFunctionalExtensions;
using FastEndpoints.Security;
using Microsoft.Extensions.Configuration;
using PopcornHub.Application.IServices;
using PopcornHub.Domain.Entities;
using PopcornHub.Shared.Configurations;

namespace PopcornHub.Application.Services;
public class TokenService : ITokenService
{
    private readonly IConfigurationService _config;

    public TokenService(IConfigurationService config)
    {
        _config = config;
    }

    public Result<string> GenerateToken(User user)
    {
        try
        {
            var token = JwtBearer.CreateToken(options =>
            {
                options.SigningKey = _config.JwtSettings.SigningKey;
                options.ExpireAt = DateTime.UtcNow.AddDays(7);
                options.User["UserId"] = user.Id.ToString();
                options.User["UserName"] = user.UserName;
            });

            return Result.Success(token);
        }
        catch (Exception ex)
        {
            return Result.Failure<string>($"Token generation failed: {ex.Message}");
        }
    }
}