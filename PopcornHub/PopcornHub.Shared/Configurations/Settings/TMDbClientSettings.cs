using System.ComponentModel.DataAnnotations;

namespace PopcornHub.Shared.Configurations.Settings;

public class TMDbClientSettings
{
    [Required(ErrorMessage = "ApiKey is required.")]
    public string ApiKey { get; set; } = string.Empty;
}