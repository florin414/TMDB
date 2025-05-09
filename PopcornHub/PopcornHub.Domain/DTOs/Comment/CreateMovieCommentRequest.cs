using PopcornHub.Domain.Entities;
using Volo.Abp.Application.Dtos;

namespace PopcornHub.Domain.DTOs.Comment;

public class CreateMovieCommentRequest : EntityDto
{
    public int MovieId { get; private set; }
    public string Text { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int UserId { get; private set; }
    
    public User User { get; private set; }

    public CreateMovieCommentRequest(int movieId, int userId, string text)
    {
        MovieId = movieId;
        UserId = userId;
        Text = text;
        CreatedAt = DateTime.UtcNow;
    }
}