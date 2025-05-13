using PopcornHub.Application.IStrategies;
using PopcornHub.Domain.DTOs.Movie;

namespace PopcornHub.Application.Strategies;

public class MovieSortStrategy : IMovieSortStrategy
{
    private readonly Func<MovieDto, object?> _keySelector;

    public MovieSortStrategy(Func<MovieDto, object?> keySelector)
    {
        _keySelector = keySelector;
    }

    public IOrderedEnumerable<MovieDto> ExecuteStrategy(IEnumerable<MovieDto> movies)
    {
        return movies.OrderByDescending(_keySelector);
    }
}