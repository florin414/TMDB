namespace PopcornHub.Domain.Models.Auth;

public class LoginModel
{
    public LoginModel(string email, string password)
    {
        Email = email;
        Password = password;
    }
    
    public string Email { get; init; }
    
    public string Password { get; init; }
}
