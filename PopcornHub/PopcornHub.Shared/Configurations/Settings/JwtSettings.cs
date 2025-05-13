using System.ComponentModel.DataAnnotations;

namespace PopcornHub.Shared.Configurations.Settings;

public class JwtSettings
{
    [Required(ErrorMessage = "SecretKey is required.")]
    public string SigningKey { get; set; } = string.Empty;
}