namespace PopcornHub.Domain.Models.Movie;

public class GetMovieCommentsModel
{
    public List<MovieCommentModel> MovieComments { get; init; }
    
    public int? NextCursor { get; init; }
}