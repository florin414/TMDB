using PopcornHub.Shared.Configurations.Settings;

namespace PopcornHub.Shared.Configurations;

public interface IConfigurationService
{
    JwtSettings JwtSettings { get; }
    
    ConnectionStringsSettings ConnectionStringsSettings { get; }
    
    TMDbClientSettings TMDbClientSettings { get; }
    
    CorsSettings CorsSettings { get; }
}