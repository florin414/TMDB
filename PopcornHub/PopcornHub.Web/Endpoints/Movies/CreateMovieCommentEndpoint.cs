using CSharpFunctionalExtensions.ValueTasks;
using FastEndpoints;
using PopcornHub.Application.IServices;
using PopcornHub.Domain.DTOs.ApiError;
using PopcornHub.Domain.DTOs.Comment;
using PopcornHub.Domain.ValueObjects;
using PopcornHub.Shared.Constants;
using PopcornHub.Web.DomainMappings.Movie;

namespace PopcornHub.Web.Endpoints.Movies;

public class CreateMovieCommentEndpoint : Endpoint<MovieCommentRequest, MovieCommentResponse>
{
    private readonly ICommentService _commentService;

    public CreateMovieCommentEndpoint(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public override void Configure()
    {
        Post("/{id:int}/comment");
        Group<MoviesGroup>();
        Summary(s =>
        {
            s.Summary = "Add a new comment to a specific movie.";
            s.Description = "Creates and stores a new user comment associated with the specified movie ID. " +
                            "The comment typically includes user feedback such as a message, rating, or timestamp.";
            s.Response<MovieCommentResponse>(StatusCodes.Status200OK, "Comment created successfully.");
            s.Response<ApiErrorResponse>(StatusCodes.Status400BadRequest, "Invalid input or missing required fields.");
            s.Response<ApiErrorResponse>(StatusCodes.Status404NotFound, "Movie not found.");
            s.Response<ApiErrorResponse>(StatusCodes.Status401Unauthorized, "User is not authenticated or the token is invalid.");
        });
    }

    public override async Task HandleAsync(MovieCommentRequest req, CancellationToken ct)
    {
        var movieId = new MovieId(Route<int>(RouteParameterKeys.Id));
        req.MovieId = movieId;
        var result = await _commentService.CreateMovieCommentAsync(req.ToModel());
        
        await result.Match(
            async success => await SendAsync(success.ToResponse(), StatusCodes.Status200OK, ct),
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