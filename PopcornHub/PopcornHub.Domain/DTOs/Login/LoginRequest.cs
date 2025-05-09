using Volo.Abp.Application.Dtos;

namespace PopcornHub.Domain.DTOs.Login;

public class LoginRequest : EntityDto
{
    public string Username { get; set; }
    
    public string Password { get; set; }
}