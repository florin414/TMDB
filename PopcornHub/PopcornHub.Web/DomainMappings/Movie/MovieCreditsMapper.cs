using PopcornHub.Domain.DTOs.Movie;
using PopcornHub.Domain.Models.Movie;

namespace PopcornHub.Web.DomainMappings.Movie;

public static class MovieCreditsMapper
{
    public static MovieCreditsResponse ToResponse(this MovieCreditsModel model)
        => new()
        {
            Credits = model.Credits,
        };
}