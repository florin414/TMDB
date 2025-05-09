using Volo.Abp.Application.Dtos;

namespace PopcornHub.Domain.DTOs.Login;

public class LoginResponse : EntityDto
{
    public string Token { get; set; }
}