namespace PopcornHub.Domain.Models.Auth;

public class RegisterModel
{
    public RegisterModel(string userName, string email, string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }

    public string UserName { get; init; }
    
    public string Email { get; init; }
    
    public string Password { get; init; }
}