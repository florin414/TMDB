namespace PopcornHub.Domain.DTOs.Auth;

public class RegisterRequest 
{
    public string Email { get; init; }
    
    public string Password { get; init; }
    
    public string UserName { get; init; }
}