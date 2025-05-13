using PopcornHub.Domain.DTOs.Movie;

namespace PopcornHub.Domain;

public interface IMovieApiClient
{
    Task<MovieDto> GetMovieAsync(int movieId); 

    Task<List<GenreDto>> GetMovieGenresAsync(); 

    Task<CreditsDto> GetMovieCreditsAsync(int moveId); 

    Task<List<MovieDto>> GetMovieTopRatedListAsync(); 

    Task<List<MovieDto>> GetMovieLatestReleasedListAsync(); 

    Task<List<MovieDto>> GetMoviesGenresListAsync(string genre);

    Task<List<MovieDto>>  SearchMovieByGenreListAsync(string name, string genre);

    Task<List<MovieDto>>  SearchMovieAsync(string name);
}