using FluentValidation.Results;
using PopcornHub.Domain.DTOs.Auth;

namespace PopcornHub.Web.Validators;

public static class RegisterRequestValidator
{
    public static void Validate(RegisterRequest req, Action<ValidationFailure> addFailure)
    {
        CommonValidator.ValidateEmail(req.Email, addFailure);
        CommonValidator.ValidatePassword(req.Password, addFailure);
        CommonValidator.ValidateUserName(req.UserName, addFailure);
    }
}