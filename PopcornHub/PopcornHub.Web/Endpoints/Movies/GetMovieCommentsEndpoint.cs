using CSharpFunctionalExtensions.ValueTasks;
using FastEndpoints;
using PopcornHub.Application.IServices;
using PopcornHub.Domain.DTOs.ApiError;
using PopcornHub.Domain.DTOs.Comment;
using PopcornHub.Domain.ValueObjects;
using PopcornHub.Shared.Constants;
using PopcornHub.Web.DomainMappings.Movie;

namespace PopcornHub.Web.Endpoints.Movies;

public class GetMovieCommentsEndpoint : Endpoint<MovieCommentsRequest, MovieCommentsResponse>
{
    private readonly ICommentService _commentService;

    public GetMovieCommentsEndpoint(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public override void Configure()
    {
        Get("/{id:int}/comments");
        Group<MoviesGroup>();
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Retrieve comments for a specific movie.";
            s.Description = "This endpoint allows you to fetch all comments associated with a particular movie, identified by its MovieId. " +
                            "The response includes the list of comments for the given movie."; 
            s.Response<MovieCommentsResponse>(StatusCodes.Status200OK, "Get movie comments"); 
            s.Response<ApiErrorResponse>(StatusCodes.Status400BadRequest, "Invalid input or missing required fields.");
            s.Response<ApiErrorResponse>(StatusCodes.Status404NotFound, "Movie not found.");
        });
    }

    public override async Task HandleAsync(MovieCommentsRequest req, CancellationToken ct)
    {
        var movieId = new MovieId(Route<int>(RouteParameterKeys.Id));
        req.MovieId = movieId;
        var result = await _commentService.GetMovieCommentsAsync(req.ToModel());
        
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