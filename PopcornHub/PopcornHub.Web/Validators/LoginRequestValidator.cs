using FluentValidation.Results;
using PopcornHub.Domain.DTOs.Auth;

namespace PopcornHub.Web.Validators;

public static class LoginRequestValidator
{
    public static void Validate(LoginRequest req, Action<ValidationFailure> addFailure)
    {
        CommonValidator.ValidateEmail(req.Email, addFailure);
        CommonValidator.ValidatePassword(req.Password, addFailure);
    }
}