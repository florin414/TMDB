using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PopcornHub.Application.EntityMapping;
using PopcornHub.Application.IServices;
using PopcornHub.Application.Strategies;
using PopcornHub.Domain;
using PopcornHub.Domain.DTOs.Movie;
using PopcornHub.Domain.IServices;
using PopcornHub.Domain.Models.Movie;
using PopcornHub.Shared.Constants;
using TMDbLib.Objects.Search;

namespace PopcornHub.Application.Services;

public class MovieService : IMovieService
{
    private readonly ILogger<MovieService> _logger;
    private readonly IMovieExistenceChecker _movieExistenceChecker;
    private readonly IMovieApiClient _movieApiClient;

    public MovieService(ILogger<MovieService> logger, IMovieExistenceChecker movieExistenceChecker, IMovieApiClient movieApiClient)
    {
        _logger = logger;
        _movieExistenceChecker = movieExistenceChecker;
        _movieApiClient = movieApiClient;
    }
    
    public async Task<Result<MovieGenresModel>> GetMovieGenresAsync()
    {
        try
        {
            var genres = await _movieApiClient.GetMovieGenresAsync();
            
            return Result.Success(new MovieGenresModel
            {
                Genres = genres
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching movie genres in GetMovieGenresAsync. External client may be unreachable or returned invalid data.");

            return Result.Failure<MovieGenresModel>(MovieFailureCodes.FailedToFetchMovieGenres);
        }
    }

    public async Task<Result<MovieCreditsModel>> GetMovieCreditsAsync(MovieCreditsModel model)
    {
        try
        {
            var movieExists = await _movieExistenceChecker.ExistsAsync(model.MovieId);
            if (!movieExists)
            {
                return Result.Failure<MovieCreditsModel>(MovieFailureCodes.MovieNotFound); 
            }
            
            var credits = await _movieApiClient.GetMovieCreditsAsync(model.MovieId.Value);
            
            model.Credits = credits;
            
            return Result.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching movie credits for MovieId={MovieId}", model.MovieId);

            return Result.Failure<MovieCreditsModel>(MovieFailureCodes.FailedToFetchMovieCredits);
        }
    }
    
    public async Task<Result<GetMoviesModel>> GetMoviesAsync(MoviesModel model)
    {
        try
        {
            List<MovieDto> movies = [];
            
            if (!string.IsNullOrWhiteSpace(model.Name) && !string.IsNullOrWhiteSpace(model.Genre))
            {
                movies = await _movieApiClient.SearchMovieByGenreListAsync(model.Name, model.Genre);
            }
            
            if (!string.IsNullOrWhiteSpace(model.Genre))
            {
                movies = await _movieApiClient.GetMoviesGenresListAsync(model.Genre);
            }
            
            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                movies = await _movieApiClient.SearchMovieAsync(model.Name);
            }

            var hasFilters = !string.IsNullOrWhiteSpace(model.Name) || !string.IsNullOrWhiteSpace(model.Genre);
            
            if (movies.Count != 0 && hasFilters)
            {
                movies = SortMovies(movies, model.SortBy);
            }
            
            if(movies.Count == 0 && !hasFilters)
            {
                movies = await GetFallbackMoviesAsync(_movieApiClient, model.SortBy!);
            }
            
            var paginatedMovies = movies
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToList();

            return Result.Success(new GetMoviesModel
            {
                Movies = paginatedMovies,
                Page = model.Page,
                PageSize = model.PageSize,
                TotalCount = movies.Count 
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error during SearchMoviesAsync");

            return Result.Failure<GetMoviesModel>("Failed to fetch movies filtred my name and genres");
        }
    }
    
    private static List<MovieDto> SortMovies(List<MovieDto> movies, string? sortBy)
    {
        if (string.IsNullOrEmpty(sortBy))
            return movies;

        var sortStrategy = MovieSortStrategyFactory.Create(sortBy);
        var sortedMovies = sortStrategy.ExecuteStrategy(movies).ToList();

        return sortedMovies;
    }
    
    private static async Task<List<MovieDto>> GetFallbackMoviesAsync(IMovieApiClient client, string sortBy)
    {
        if (!sortBy.Contains(MovieSortCriteria.LatestReleased))
        {
            if (!sortBy.Contains(MovieSortCriteria.TopRated)) return [];
            var topMovies = await client.GetMovieTopRatedListAsync();
            return topMovies;
        }

        var result = await client.GetMovieLatestReleasedListAsync();
        return result;
    }
}