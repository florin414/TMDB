namespace PopcornHub.Shared.Configuration;

public interface IConfigurationService
{
    public static string ApiReadAccessToken { get; set; }
    public static string ApiKey { get; set; }
}