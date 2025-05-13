using PopcornHub.Domain.ValueObjects;

namespace PopcornHub.Domain.Models.Movie;

public class MovieCommentModel
{
    public int Id { get; init; }
    
    public MovieId MovieId { get; init; }
    
    public Comment Comment { get; init; }
    
    public DateTime CreatedAt { get; init; }
}