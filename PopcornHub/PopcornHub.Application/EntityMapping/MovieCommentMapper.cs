using PopcornHub.Domain.DTOs.Comment;
using PopcornHub.Domain.Entities;
using PopcornHub.Domain.Models.Movie;

namespace PopcornHub.Application.EntityMapping;

public static class MovieCommentMapper
{
    public static MovieCommentModel ToModel(this MovieComment entity) =>
        new()
        {
            Id = entity.Id,
            MovieId = entity.MovieId,
            Comment = entity.Comment,
            CreatedAt = entity.CreatedAt,
        };

    public static MovieComment ToEntity(this MovieCommentModel model) =>
        new MovieComment(model.MovieId, model.Comment, Guid.Empty);
}