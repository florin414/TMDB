using CSharpFunctionalExtensions;
using PopcornHub.Domain.Models.Movie;

namespace PopcornHub.Application.IServices;

public interface ICommentService 
{   
    Task<Result<MovieCommentModel>> CreateMovieCommentAsync(MovieCommentModel model);
    
    Task<Result<GetMovieCommentsModel>> GetMovieCommentsAsync(MovieCommentsModel model);
}