using PopcornHub.Domain.DTOs.Movie;
using TMDbLib.Objects.Movies;

namespace PopcornHub.Application.EntityMapping;

public static class CreditsMapper
{
    public static CreditsDto ToDto(this Credits credits)
    {
        return new CreditsDto
        {
            MovieId = credits.Id,
            Cast = credits.Cast?.Select(c => new CastDto
            {
                Id = c.Id,
                Name = c.Name,
                Character = c.Character,
                ProfilePath = c.ProfilePath
            }).ToList() ?? [],
        };
    }
}
