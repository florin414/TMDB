using PopcornHub.Domain.Models.Movie;
using PopcornHub.Domain.ValueObjects;

namespace PopcornHub.UnitTests.TestUtils;

public static class MovieCommentModelFactory
{
    public static MovieCommentModel CreateValid()
    {
        return new MovieCommentModel
        {
            MovieId = new MovieId(1),
            Comment = new Comment("Great movie!")
        };
    }

    public static MovieCommentModel CreateInvalid() 
    {
        return new MovieCommentModel();
    }
}
