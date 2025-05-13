using CSharpFunctionalExtensions;
using FastEndpoints;
using PopcornHub.Application.IServices;
using PopcornHub.Domain.DTOs.ApiError;
using PopcornHub.Domain.DTOs.Movie;
using PopcornHub.Web.DomainMappings.Movie;

namespace PopcornHub.Web.Endpoints.Movies;

public class MoviesEndpoint : Endpoint<MoviesRequest, MoviesResponse>
{
    private readonly IMovieService _movieService;

    public MoviesEndpoint(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public override void Configure()
    {
        Get("");
        Group<MoviesGroup>();
        AllowAnonymous();
        Summary(s =>
        {       
            s.Summary = "Search movies by name and/or genre.";
            s.Description = "Returns movies that match the provided name and/or genre.";
            s.Response<MoviesResponse>(StatusCodes.Status200OK, "Movies found"); 
            s.Response<ApiErrorResponse>(StatusCodes.Status400BadRequest, "Bad request");
        });
    }

    public override async Task HandleAsync(MoviesRequest req, CancellationToken ct)
    {
        var result = await _movieService.GetMoviesAsync(req.ToModel());
        
        await result.Match(
            async success => await SendOkAsync(success.ToResponse(), ct), 
            async error => await SendErrorsAsync(StatusCodes.Status400BadRequest, ct)
        );
    }
}
