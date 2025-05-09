namespace PopcornHub.Shared.Exceptions;

public class ConfigurationMissingException(string key)
    : ArgumentNullException($"Configuration for key '{key}' is missing.");