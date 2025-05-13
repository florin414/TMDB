using FastEndpoints;
using PopcornHub.Domain.ValueObjects;

namespace PopcornHub.Domain.DTOs.Comment;

public class MovieCommentsRequest 
{
    [QueryParam] 
    public int? Cursor { get; init; } 
    
    [QueryParam] 
    public int Limit { get; init; } 
    
    public MovieId MovieId { get; set; }
}