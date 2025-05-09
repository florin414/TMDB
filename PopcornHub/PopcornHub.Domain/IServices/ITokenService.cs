using PopcornHub.Domain.Entities;

namespace PopcornHub.Domain.IServices;

public interface ITokenService
{
    Task<string> GenerateJwtTokenAsync(User user);
    
    Task<string> GenerateRefreshTokenAsync(User user);
}