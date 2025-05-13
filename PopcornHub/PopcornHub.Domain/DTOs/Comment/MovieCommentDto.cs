using PopcornHub.Domain.ValueObjects;

namespace PopcornHub.Domain.DTOs.Comment;

public class MovieCommentDto
{
    public int Id { get; init; }
    
    public MovieId MovieId { get; init; }
    
    public ValueObjects.Comment Comment { get; init; }
    
    public DateTime CreatedAt { get; init; }
}