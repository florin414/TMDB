using FluentValidation.Results;

namespace PopcornHub.Web.Validators;

public static class CommonValidator
{
    public static void ValidateEmail(string email, Action<ValidationFailure> addFailure)
    {
        if (string.IsNullOrWhiteSpace(email))
            addFailure(new ValidationFailure(nameof(email), "Email must be provided."));
        else if (!email.Contains("@") || !email.Contains("."))
            addFailure(new ValidationFailure(nameof(email), "Email must be a valid email address."));
    }

    public static void ValidatePassword(string password, Action<ValidationFailure> addFailure)
    {
        if (string.IsNullOrWhiteSpace(password))
            addFailure(new ValidationFailure(nameof(password), "Password must be provided."));
        else if (password.Length < 6 || password.Length > 100)
            addFailure(new ValidationFailure(nameof(password), "Password must be between 6 and 100 characters long."));
    }

    public static void ValidateUserName(string userName, Action<ValidationFailure> addFailure)
    {
        if (string.IsNullOrWhiteSpace(userName))
            addFailure(new ValidationFailure(nameof(userName), "Username must be provided."));
        else if (userName.Length < 3 || userName.Length > 50)
            addFailure(new ValidationFailure(nameof(userName), "Username must be between 3 and 50 characters long."));
        else if (userName != userName.Trim())
            addFailure(new ValidationFailure(nameof(userName), "Username cannot start or end with whitespace."));
    }
}