using CSharpFunctionalExtensions;
using PopcornHub.Domain.DTOs.Movies;
using PopcornHub.Domain.Entities;
using Volo.Abp.Application.Services;

namespace PopcornHub.Domain.IServices;

public interface IMovieService 
{
    Task<Result<SearchMoviesResponse>> SearchMoviesAsync(SearchMoviesRequest request);
    Task<Result<MovieCreditsResponse>> GetMovieCreditsAsync(MovieCreditsRequest request);
    Task<Result<MovieGenresResponse>> GetMovieGenresAsync();
}