namespace PopcornHub.Domain.DTOs.Comment;

public class MovieCommentsResponse 
{
    public List<MovieCommentDto> MovieComments { get; init; } 
    
    public int? NextCursor { get; init; }
}