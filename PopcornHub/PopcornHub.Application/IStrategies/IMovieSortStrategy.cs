using PopcornHub.Domain.DTOs.Movie;

namespace PopcornHub.Application.IStrategies;

public interface IMovieSortStrategy
{
    IOrderedEnumerable<MovieDto> ExecuteStrategy(IEnumerable<MovieDto> movies);
}
