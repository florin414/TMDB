using System.Net;
using CSharpFunctionalExtensions.ValueTasks;
using FastEndpoints;
using PopcornHub.Domain.DTOs.ApiError;
using PopcornHub.Domain.DTOs.Comment;
using PopcornHub.Domain.IServices;
using PopcornHub.Web.Endpoints.Movies;

namespace PopcornHub.Web.Endpoints.Comments;

public class CreateMovieCommentEndpoint : Endpoint<CreateMovieCommentRequest, CreateMovieCommentResponse>
{
    private readonly ICommentService _commentService;

    public CreateMovieCommentEndpoint(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public override void Configure()
    {
        Post("comments");
        Group<MoviesGroup>();
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "";
            s.Description = "";
            s.Response<CreateMovieCommentResponse>(200, "Create movie comment"); 
            s.Response<ApiErrorResponse>(400, "Bad request");
        });
    }

    public override async Task HandleAsync(CreateMovieCommentRequest req, CancellationToken ct)
    {
        var result = await _commentService.CreateMovieCommentAsync(req);
        
        // MapToResponse
        await result.Match(
            async success => await SendOkAsync(success, ct), 
            async error => await SendErrorsAsync((int)HttpStatusCode.BadRequest, ct)
        );
    }
}