using PopcornHub.Domain.ValueObjects;

namespace PopcornHub.Domain.DTOs.Comment;

public class MovieCommentRequest 
{
    public string Comment { get; init; }
    
    public MovieId MovieId { get; set; }
}