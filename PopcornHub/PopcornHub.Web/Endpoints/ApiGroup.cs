using FastEndpoints;

namespace PopcornHub.Web.Endpoints;

public sealed class ApiGroup : Group
{
    public ApiGroup()
    {
        Configure("/api", definition =>
        {
            definition.Description(x =>
            {
                // Nu adăuga tag-ul global aici
            });
        });
    }
}
