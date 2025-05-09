using System.Net;
using CSharpFunctionalExtensions;
using FastEndpoints;
using PopcornHub.Domain.DTOs;
using PopcornHub.Domain.DTOs.ApiError;
using PopcornHub.Domain.DTOs.Movies;
using PopcornHub.Domain.IServices;

namespace PopcornHub.Web.Endpoints.Movies;

public class SearchMoviesEndpoint : Endpoint<SearchMoviesRequest, SearchMoviesResponse>
{
    private readonly IMovieService _movieService;

    public SearchMoviesEndpoint(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public override void Configure()
    {
        Get("/search");
        Group<MoviesGroup>();
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Search movies by name and/or genre.";
            s.Description = "Returns movies that match the provided name and/or genre.";
            s.Response<SearchMoviesResponse>(200, "Movies found"); 
            s.Response<ApiErrorResponse>(400, "Bad request");
            s.RequestParam(r => r.Name, "Movie name");
            s.RequestParam(r => r.Genre, "Movie genre");
        });
    }

    public override async Task HandleAsync(SearchMoviesRequest req, CancellationToken ct)
    {
        var result = await _movieService.SearchMoviesAsync(req);
        
        // MapToResponse
        await result.Match(
            async success => await SendOkAsync(success, ct), 
            async error => await SendErrorsAsync((int)HttpStatusCode.BadRequest, ct)
        );
    }
}
