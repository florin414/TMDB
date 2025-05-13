using System.ComponentModel.DataAnnotations;

namespace PopcornHub.Shared.Configurations.Settings;


public class ConnectionStringsSettings
{
    [Required(ErrorMessage = "The PostgresSQL connection string is required.")]
    public string PostgresConnectionString { get; set; } = string.Empty;
}