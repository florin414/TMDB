using Microsoft.Extensions.Configuration;
using PopcornHub.Shared.Exceptions;

namespace PopcornHub.Shared.Configuration;

public class ConfigurationService : IConfigurationService
{
    private readonly IConfiguration _configuration;

    public ConfigurationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string ApiReadAccessToken => _configuration[nameof(ApiReadAccessToken)] ?? 
                                        throw new ConfigurationMissingException(nameof(ApiReadAccessToken));
    
    public string ApiKey => _configuration[nameof(ApiKey)] ?? 
                            throw new ConfigurationMissingException(nameof(ApiKey));
}