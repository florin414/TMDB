using PopcornHub.Domain.DTOs.Auth;
using PopcornHub.Domain.Models.Auth;

namespace PopcornHub.Web.DomainMappings.Auth;

public static class RegisterMapper
{
    public static RegisterModel ToModel(this RegisterRequest request)
        => new RegisterModel(request.UserName, request.Email, request.Password);
}