using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PopcornHub.Shared.Configurations.Settings;
using PopcornHub.Shared.Exceptions;
using PopcornHub.Shared.Utils;

namespace PopcornHub.Shared.Configurations;

public class ConfigurationService : IConfigurationService
{
    public JwtSettings JwtSettings { get; }
    
    public ConnectionStringsSettings ConnectionStringsSettings { get; }
    
    public TMDbClientSettings TMDbClientSettings { get; }
    
    public CorsSettings CorsSettings { get; }

    public ConfigurationService(
        IOptions<JwtSettings> jwtOptions,
        IOptions<ConnectionStringsSettings> connectionOptions,
        IOptions<TMDbClientSettings> tmDbClientOptions,
        IOptions<CorsSettings> corsOptions)
    {
        JwtSettings = 
            AssertConfiguration.NotNullOrEmpty(jwtOptions.Value, nameof(JwtSettings));
        
        ConnectionStringsSettings = 
            AssertConfiguration.NotNullOrEmpty(connectionOptions.Value, nameof(ConnectionStringsSettings));
        
        TMDbClientSettings = 
            AssertConfiguration.NotNullOrEmpty(tmDbClientOptions.Value, nameof(TMDbClientSettings));
        
        CorsSettings =
            AssertConfiguration.NotNullOrEmpty(corsOptions.Value, nameof(CorsSettings));
    }
}