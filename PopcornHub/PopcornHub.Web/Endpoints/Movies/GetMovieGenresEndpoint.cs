using CSharpFunctionalExtensions.ValueTasks;
using FastEndpoints;
using PopcornHub.Application.IServices;
using PopcornHub.Domain.DTOs.ApiError;
using PopcornHub.Domain.DTOs.Movie;
using PopcornHub.Web.DomainMappings.Movie;

namespace PopcornHub.Web.Endpoints.Movies;

public class GetMovieGenresEndpoint : EndpointWithoutRequest<MovieGenresResponse>
{
    private readonly IMovieService _movieService;

    public GetMovieGenresEndpoint(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public override void Configure()
    {
        Get("/genres");
        Group<MoviesGroup>();
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Retrieve all movie genres.";
            s.Description = "Returns a list of all available movie genres from the database.";
            s.Response<MovieGenresResponse>(StatusCodes.Status200OK, "List of genres retrieved successfully");
            s.Response<ApiErrorResponse>(StatusCodes.Status400BadRequest, "Invalid request or data retrieval failed");
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _movieService.GetMovieGenresAsync();
        
        await result.Match(
            async success => await SendOkAsync(success.ToResponse(), ct), 
            async error => await SendErrorsAsync(StatusCodes.Status400BadRequest, ct)
        );
    }   
}