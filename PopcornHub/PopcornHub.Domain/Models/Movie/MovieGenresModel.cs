using PopcornHub.Domain.DTOs.Movie;

namespace PopcornHub.Domain.Models.Movie;

public class MovieGenresModel 
{
    public List<GenreDto> Genres  { get; init; }
}