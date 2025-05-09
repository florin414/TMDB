using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PopcornHub.Domain.DTOs.Movies;
using PopcornHub.Domain.IServices;
using TMDbLib.Client;
using TMDbLib.Objects.Discover;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace PopcornHub.Application.Services;

public class MovieService : IMovieService
{
    private readonly TMDbClient _client;
    private readonly ILogger<MovieService> _logger;

    public MovieService(ILogger<MovieService> logger, TMDbClient client)
    {
        _logger = logger;
        _client = client;
    }
    
    public async Task<Result<MovieGenresResponse>> GetMovieGenresAsync()
    {
        try
        {
            List<Genre> genres = await _client.GetMovieGenresAsync();
            
            return Result.Success(new MovieGenresResponse
            {
                Genres = genres,
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error during GetMovieGenresAsync");

            return Result.Failure<MovieGenresResponse>("Failed to fetch movie genres");
        }
    }

    public async Task<Result<MovieCreditsResponse>> GetMovieCreditsAsync(MovieCreditsRequest request)
    {
        try
        {
            Credits credits = await _client.GetMovieCreditsAsync(request.MovieId);
            
            return Result.Success(new MovieCreditsResponse
            {
                Credits = credits,
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error during GetMovieDetailsAsync");

            return Result.Failure<MovieCreditsResponse>("Failed to fetch movie details");
        }
    }
    
    public async Task<Result<SearchMoviesResponse>> SearchMoviesAsync(SearchMoviesRequest request)
    {
        try
        {
            List<SearchMovie> movies = [];

            // filter 
            if (!string.IsNullOrEmpty(request.Name) && !string.IsNullOrEmpty(request.Genre))
            {
                SearchContainer<SearchMovie> searchMovies = await _client.SearchMovieAsync(request.Name);
                List<Genre> genres = await _client.GetMovieGenresAsync();
                int genreId = genres.First(g => g.Name == request.Genre).Id;
                List<SearchMovie> filtredMovieByNameAndGenre = searchMovies
                    .Results
                    .Where(m => m.GenreIds.Contains(genreId))
                    .ToList();
                
                movies.AddRange(filtredMovieByNameAndGenre);
            }
            else if (!string.IsNullOrEmpty(request.Name))
            {
                SearchContainer<SearchMovie> searchMovies = await _client.SearchMovieAsync(request.Name);
                movies.AddRange(searchMovies.Results);
            }
            else if (!string.IsNullOrEmpty(request.Genre))
            {
                List<Genre> genres = await _client.GetMovieGenresAsync();
                Genre? genre = genres.FirstOrDefault(g => g.Name == request.Genre);
                
                if (genre != null)
                {
                    DiscoverMovie discoverMovie = _client.DiscoverMoviesAsync().IncludeWithAllOfGenre([genre]);
                    SearchContainer<SearchMovie> searchMovies = discoverMovie.Query().Result;
                    movies.AddRange(searchMovies.Results);
                }
            }
            
            // sort
            
            if (request.Latest || request.Top)
            {
                IOrderedEnumerable<SearchMovie>? sorted = null;

                if (request.Top)
                {
                    sorted = sorted == null
                        ? movies.OrderByDescending(m => m.VoteAverage)
                        : sorted.ThenByDescending(m => m.VoteAverage);
                }
                if (request.Latest)
                {
                    sorted = sorted == null
                        ? movies.OrderByDescending(m => m.ReleaseDate)
                        : sorted.ThenByDescending(m => m.ReleaseDate);
                }
                
                if (sorted != null)
                    movies = sorted.ToList();
            }
            
            return Result.Success(new SearchMoviesResponse
            {
                Movies = movies,
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error during SearchMoviesAsync");

            return Result.Failure<SearchMoviesResponse>("Failed to fetch movies filtred my name and genres");
        }
    }
}