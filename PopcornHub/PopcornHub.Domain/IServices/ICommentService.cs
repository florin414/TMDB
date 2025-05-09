using CSharpFunctionalExtensions;
using PopcornHub.Domain.DTOs.Comment;
using PopcornHub.Domain.DTOs.Movies;
using Volo.Abp.Application.Services;

namespace PopcornHub.Domain.IServices;

public interface ICommentService : IApplicationService
{   
    Task<Result<CreateMovieCommentResponse>> CreateMovieCommentAsync(CreateMovieCommentRequest request);
    
    Task<Result<GetMovieCommentsResponse>> GetMovieCommentsAsync(GetMovieCommentsRequest request);
}