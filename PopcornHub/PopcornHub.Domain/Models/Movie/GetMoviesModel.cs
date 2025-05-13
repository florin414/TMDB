using PopcornHub.Domain.DTOs.Movie;

namespace PopcornHub.Domain.Models.Movie;

public class GetMoviesModel
{
    public List<MovieDto> Movies { get; init; }

    public int TotalCount { get; init; }

    public int Page { get; init; }

    public int PageSize { get; init; }
}