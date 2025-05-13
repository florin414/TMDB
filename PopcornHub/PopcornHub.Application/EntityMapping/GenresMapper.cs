using PopcornHub.Domain.DTOs.Movie;
using TMDbLib.Objects.General;

namespace PopcornHub.Application.EntityMapping;

public static class GenreMapper
{
    public static GenreDto ToDto(this Genre genre) =>
        new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name
        };

    public static List<GenreDto> ToDtoList(this List<Genre> genres) =>
        genres.Select(ToDto).ToList();
}