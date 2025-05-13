using System.ComponentModel.DataAnnotations;

namespace PopcornHub.Shared.Configurations.Settings;

public class CorsSettings
{
    [Required(ErrorMessage = "The AllowedOrigins field is required.")]
    public string AllowedOrigins { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Policy field is required.")]
    public string Policy { get; set; } = string.Empty;
}