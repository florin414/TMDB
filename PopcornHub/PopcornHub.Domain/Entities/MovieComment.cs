using PopcornHub.Domain.Common;
using PopcornHub.Domain.ValueObjects;

namespace PopcornHub.Domain.Entities;

public class MovieComment : BaseEntity<int>
{
    public MovieId MovieId { get; private set; }
    
    public Comment Comment { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; private set; }
    
    public MovieComment() {}

    public MovieComment(MovieId movieId, Comment comment, Guid userId)
    {
        MovieId = movieId;
        Comment = comment;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }
}
