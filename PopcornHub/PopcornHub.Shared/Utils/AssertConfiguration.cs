using PopcornHub.Shared.Exceptions;

namespace PopcornHub.Shared.Utils;

public static class AssertConfiguration
{
    public static T NotNullOrEmpty<T>(T input, string name) where T : class
    {
        return input switch
        {
            null => throw new ConfigurationMissingException(name),
            IEnumerable<object> enumerable when !enumerable.Any() => throw new ConfigurationMissingException(name),
            _ => input
        };
    }
}
