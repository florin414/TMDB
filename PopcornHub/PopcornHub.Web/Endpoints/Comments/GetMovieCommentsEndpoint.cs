using System.Net;
using CSharpFunctionalExtensions.ValueTasks;
using PopcornHub.Domain.DTOs.ApiError;
using PopcornHub.Domain.DTOs.Comment;
using PopcornHub.Domain.IServices;
using FastEndpoints;
using PopcornHub.Web.Endpoints.Movies;

namespace PopcornHub.Web.Endpoints.Comments;

public class GetMovieCommentsEndpoint : Endpoint<GetMovieCommentsRequest, GetMovieCommentsResponse>
{
    private readonly ICommentService _commentService;

    public GetMovieCommentsEndpoint(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public override void Configure()
    {
        Get("/comment/{id:int}");
        Group<MoviesGroup>();
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "";
            s.Description = "";
            s.Response<GetMovieCommentsResponse>(200, "Get movie comments"); 
            s.Response<ApiErrorResponse>(400, "Bad request");
        });
    }

    public override async Task HandleAsync(GetMovieCommentsRequest req, CancellationToken ct)
    {
        var result = await _commentService.GetMovieCommentsAsync(req);
        
        // MapToResponse
        await result.Match(
            async success => await SendOkAsync(success, ct), 
            async error => await SendErrorsAsync((int)HttpStatusCode.BadRequest, ct)
        );
    }
}