using FastEndpoints;

namespace PopcornHub.Web.Endpoints.Auth;

public sealed class AuthGroup : Group
{
    public AuthGroup()
    {
        Configure("/api/auth", definition =>
        {
            definition.Description(x =>
            {
                x.Produces(StatusCodes.Status400BadRequest)
                    .WithTags("Auth");
            });
        });
    }
}