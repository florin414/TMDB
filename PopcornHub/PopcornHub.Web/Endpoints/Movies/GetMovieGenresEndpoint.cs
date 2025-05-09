using System.Net;
using CSharpFunctionalExtensions.ValueTasks;
using FastEndpoints;
using PopcornHub.Domain.DTOs;
using PopcornHub.Domain.DTOs.ApiError;
using PopcornHub.Domain.DTOs.Movies;
using PopcornHub.Domain.IServices;

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
            s.Summary = "Search movies by name and/or genre.";
            s.Description = "Returns movies that match the provided name and/or genre.";
            s.Response<MovieGenresResponse>(200, "Credits found"); 
            s.Response<ApiErrorResponse>(400, "Bad request");
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _movieService.GetMovieGenresAsync();
        
        // MapToResponse
        await result.Match(
            async success => await SendOkAsync(success, ct), 
            async error => await SendErrorsAsync((int)HttpStatusCode.BadRequest, ct)
        );
    }   
}