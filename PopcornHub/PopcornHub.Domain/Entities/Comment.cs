using Volo.Abp.Domain.Entities;

namespace PopcornHub.Domain.Entities;

public class Comment : Entity<int>
{
    public int MovieId { get; private set; }
    public string Text { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int UserId { get; private set; }
    
    public User User { get; private set; }

    public Comment(int movieId, int userId, string text)
    {
        MovieId = movieId;
        UserId = userId;
        Text = text;
        CreatedAt = DateTime.UtcNow;
    }
}