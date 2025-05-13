using PopcornHub.Domain.ValueObjects;

namespace PopcornHub.Domain.Models.Movie;

public class MovieCommentsModel
{
    public MovieId MovieId { get; init; }
    
    public int? Cursor { get; set; }
    
    public int Limit { get; init; }
}