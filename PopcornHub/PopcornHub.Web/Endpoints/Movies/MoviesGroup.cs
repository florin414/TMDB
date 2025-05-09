using FastEndpoints;

namespace PopcornHub.Web.Endpoints.Movies;

public sealed class MoviesGroup : Group
{
    public MoviesGroup()
    {
        Configure("/api/movies", definition =>
        {
            definition.Description(x =>
            {
                x.Produces(StatusCodes.Status400BadRequest)
                    .WithTags("Movies");
            });
        });
    }
}