using PopcornHub.Domain.DTOs.Comment;
using PopcornHub.Domain.Models.Movie;
using PopcornHub.Domain.ValueObjects;

namespace PopcornHub.Web.DomainMappings.Movie;

public static class MovieCommentMapper
{
    public static MovieCommentResponse ToResponse(this MovieCommentModel model)
        => new()
        {
            MovieComment = new MovieCommentDto
            {
                Id = model.Id,
                Comment = model.Comment,
                MovieId = model.MovieId,
                CreatedAt = model.CreatedAt,
            }
        };

    public static MovieCommentDto ToDto(this MovieCommentModel model)
        => new()
        {
            Id = model.Id,
            Comment = model.Comment,
            MovieId = model.MovieId,
            CreatedAt = model.CreatedAt,
        };
    
    public static MovieCommentModel ToModel(this MovieCommentRequest request)
        => new()
        {
            MovieId = request.MovieId,
            Comment = new Comment(request.Comment)
        };
    
    public static MovieCommentsModel ToModel(this MovieCommentsRequest request)
        => new()
        {
            MovieId = request.MovieId,
            Cursor = request.Cursor,
            Limit = request.Limit,
        };

    public static MovieCommentsResponse ToResponse(this GetMovieCommentsModel model)
        => new()
        {
            MovieComments = model.MovieComments.Select(x => x.ToDto()).ToList(),
            NextCursor = model.NextCursor,
        };
}