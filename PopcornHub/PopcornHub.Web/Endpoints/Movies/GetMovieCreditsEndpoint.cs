using CSharpFunctionalExtensions.ValueTasks;
using FastEndpoints;
using PopcornHub.Application.IServices;
using PopcornHub.Domain.DTOs.ApiError;
using PopcornHub.Domain.DTOs.Movie;
using PopcornHub.Domain.Models.Movie;
using PopcornHub.Domain.ValueObjects;
using PopcornHub.Shared.Constants;
using PopcornHub.Web.DomainMappings.Movie;

namespace PopcornHub.Web.Endpoints.Movies;

public class GetMovieCreditsEndpoint : EndpointWithoutRequest<MovieCreditsResponse>
{
    private readonly IMovieService _movieService;

    public GetMovieCreditsEndpoint(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public override void Configure()
    {
        Get("/{id:int}/credits");
        Group<MoviesGroup>();
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Get movie credits by movie ID.";
            s.Description = "Retrieves cast and crew information for a specific movie identified by its ID.";
            s.Response<MovieCreditsResponse>(StatusCodes.Status200OK, "Credits retrieved successfully");
            s.Response<ApiErrorResponse>(StatusCodes.Status400BadRequest, "Invalid movie ID or request format");
            s.Response<ApiErrorResponse>(StatusCodes.Status404NotFound, "Movie not found.");
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var movieId = new MovieId(Route<int>(RouteParameterKeys.Id));
        var result = await _movieService.GetMovieCreditsAsync(new MovieCreditsModel
        {
            MovieId = movieId,
        });
        
        await result.Match(
            async success => await SendOkAsync(success.ToResponse(), ct), 
            async error =>
            {
                var statusCode = error == MovieFailureCodes.MovieNotFound
                    ? StatusCodes.Status404NotFound
                    : StatusCodes.Status400BadRequest;
                
                await SendErrorsAsync(statusCode, ct);
            }
        );
    }
}