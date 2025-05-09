using System.Net;
using CSharpFunctionalExtensions.ValueTasks;
using PopcornHub.Domain.DTOs;
using PopcornHub.Domain.DTOs.Movies;
using PopcornHub.Domain.IServices;
using FastEndpoints;
using PopcornHub.Domain.DTOs.ApiError;

namespace PopcornHub.Web.Endpoints.Movies;

public class GetMovieCreditsEndpoint : Endpoint<MovieCreditsRequest, MovieCreditsResponse>
{
    private readonly IMovieService _movieService;

    public GetMovieCreditsEndpoint(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public override void Configure()
    {
        Get("/credits/{id:int}");
        Group<MoviesGroup>();
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Search movies by name and/or genre.";
            s.Description = "Returns movies that match the provided name and/or genre.";
            s.Response<MovieCreditsResponse>(200, "Credits found"); 
            s.Response<ApiErrorResponse>(400, "Bad request");
        });
    }

    public override async Task HandleAsync(MovieCreditsRequest req, CancellationToken ct)
    {
        var result = await _movieService.GetMovieCreditsAsync(req);
        
        // MapToResponse
        await result.Match(
            async success => await SendOkAsync(success, ct), 
            async error => await SendErrorsAsync((int)HttpStatusCode.BadRequest, ct)
        );
    }
}