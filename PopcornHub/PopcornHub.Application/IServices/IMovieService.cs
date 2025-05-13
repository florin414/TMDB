using CSharpFunctionalExtensions;
using PopcornHub.Domain.Models.Movie;

namespace PopcornHub.Application.IServices;

public interface IMovieService 
{
    Task<Result<GetMoviesModel>> GetMoviesAsync(MoviesModel model);
    
    Task<Result<MovieCreditsModel>> GetMovieCreditsAsync(MovieCreditsModel model);
    
    Task<Result<MovieGenresModel>> GetMovieGenresAsync();
}