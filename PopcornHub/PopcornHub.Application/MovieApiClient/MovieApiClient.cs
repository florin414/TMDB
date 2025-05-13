using PopcornHub.Application.EntityMapping;
using PopcornHub.Domain;
using PopcornHub.Domain.DTOs.Movie;
using TMDbLib.Client;
using TMDbLib.Objects.Discover;

namespace PopcornHub.Application.MovieApiClient;

public class MovieApiClient : IMovieApiClient
{
    private readonly TMDbClient _client;

    public MovieApiClient(TMDbClient client)
    {
        _client = client;
    }

    public async Task<MovieDto> GetMovieAsync(int movieId)
    {
        var movie = await _client.GetMovieAsync(movieId);
        return movie.ToDto();
    }

    public async Task<List<GenreDto>> GetMovieGenresAsync()
    {
        var genres = await _client.GetMovieGenresAsync();
        return genres.ToDtoList();
    }

    public async Task<CreditsDto> GetMovieCreditsAsync(int moveId)
    {
        var credits = await _client.GetMovieCreditsAsync(moveId);
        return credits.ToDto();
    }

    public async Task<List<MovieDto>> GetMovieTopRatedListAsync()
    {
        var movies= await _client.GetMovieTopRatedListAsync();
        return movies.Results.ToDtoList();
    }

    public async Task<List<MovieDto>> GetMovieLatestReleasedListAsync()
    {
        var discover = _client.DiscoverMoviesAsync();

        var result = await (discover
                .OrderBy(DiscoverMovieSortBy.ReleaseDateDesc)
                .WherePrimaryReleaseIsInYear(DateTime.Now.Year))
            .Query();

        return result.ToDtoList();
    }

    public async Task<List<MovieDto>> GetMoviesGenresListAsync(string genre)
    {
        var genres = await _client.GetMovieGenresAsync();
        var result = genres.FirstOrDefault(g => g.Name == genre);
        if (result == null) return [];

        var discover = _client.DiscoverMoviesAsync().IncludeWithAllOfGenre([result]);
        var searchResult = await discover.Query();
        
        return searchResult.Results.ToDtoList();
    }

    public async Task<List<MovieDto>> SearchMovieByGenreListAsync(string name, string genre)
    {
        var searchResult = await _client.SearchMovieAsync(name);
        var genres = await _client.GetMovieGenresAsync();
        var genreId = genres.First(g => g.Name == genre).Id;

        return searchResult.Results
            .Where(m => m.GenreIds.Contains(genreId))
            .ToList().ToDtoList();
    }

    public async Task<List<MovieDto>> SearchMovieAsync(string name)
    {
        var searchResult = await _client.SearchMovieAsync(name);
        return searchResult.Results.ToDtoList();
    }
}