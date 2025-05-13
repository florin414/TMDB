using PopcornHub.Domain.DTOs.Auth;
using PopcornHub.Domain.Models.Auth;

namespace PopcornHub.Web.DomainMappings.Auth;

public static class LoginMapper
{
    public static LoginModel ToModel(this LoginRequest request)
        => new LoginModel(request.Email, request.Password);
}