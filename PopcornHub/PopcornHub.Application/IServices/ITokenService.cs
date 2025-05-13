using CSharpFunctionalExtensions;
using PopcornHub.Domain.Entities;

namespace PopcornHub.Application.IServices;

public interface ITokenService 
{
    Result<string> GenerateToken(User user);
}