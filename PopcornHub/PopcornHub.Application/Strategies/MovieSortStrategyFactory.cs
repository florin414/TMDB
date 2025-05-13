using PopcornHub.Application.IStrategies;
using PopcornHub.Shared.Constants;

namespace PopcornHub.Application.Strategies;

public static class MovieSortStrategyFactory
{
    public static IMovieSortStrategy Create(string? sortBy)
    {
        if (string.IsNullOrWhiteSpace(sortBy))
            throw new ArgumentException("Sort option is required");

        return sortBy.ToLower() switch
        {
            _ when sortBy.Contains(MovieSortCriteria.TopRated) => 
                new MovieSortStrategy(m => m.VoteAverage),

            _ when sortBy.Contains(MovieSortCriteria.LatestReleased) => 
                new MovieSortStrategy(m => m.ReleaseDate),
        };
    }
}
