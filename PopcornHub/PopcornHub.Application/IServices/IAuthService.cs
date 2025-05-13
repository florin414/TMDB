using CSharpFunctionalExtensions;
using PopcornHub.Domain.Models.Auth;

namespace PopcornHub.Application.IServices;

public interface IAuthService 
{
    Task<Result> RegisterAsync(RegisterModel model);
    
    Task<Result> LoginAsync(LoginModel model);
}