using PopcornHub.Application.EntityMapping;
using PopcornHub.Domain.DTOs.Movie;
using PopcornHub.Domain.Models.Movie;

namespace PopcornHub.Web.DomainMappings.Movie;

public static class MovieGenresMapper
{
    public static MovieGenresResponse ToResponse(this MovieGenresModel model)
        => new()
        {
            Genres = model.Genres,
        };
}