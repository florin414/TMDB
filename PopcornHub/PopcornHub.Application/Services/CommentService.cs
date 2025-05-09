using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PopcornHub.Domain.DTOs.Comment;
using PopcornHub.Domain.Entities;
using PopcornHub.Domain.IRepository;
using PopcornHub.Domain.IServices;
using Volo.Abp.Application.Services;

namespace PopcornHub.Application.Services;

public class CommentService : ApplicationService, ICommentService
{
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly ILogger<CommentService> _logger;

    public CommentService(IRepositoryFactory repositoryFactory, ILogger<CommentService> logger)
    {
        _repositoryFactory = repositoryFactory;
        _logger = logger;
    }

    public async Task<Result<CreateMovieCommentResponse>> CreateMovieCommentAsync(CreateMovieCommentRequest request)
    {
        try
        {
            var commentRepository = _repositoryFactory.GetBasicRepository<Comment>();
        
            await commentRepository.InsertAsync(new Comment(request.MovieId, request.UserId, request.Text));
            
            return Result.Success(new CreateMovieCommentResponse(request.MovieId, request.UserId, request.Text));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Result.Failure<CreateMovieCommentResponse>("Failed to create movie comment");
        }
    }

    public async Task<Result<GetMovieCommentsResponse>> GetMovieCommentsAsync(GetMovieCommentsRequest request)
    {
        try
        {
            var readOnlyCommentRepository = _repositoryFactory.GetReadOnlyRepository<Comment>();
            var queryable = await readOnlyCommentRepository.GetQueryableAsync();
            
            // order by latest desc
        
            List<Comment> comments =queryable
                .Where(p => p.MovieId == request.MovieId)
                .ToList();
            
            return Result.Success(new GetMovieCommentsResponse
            {
                Comments = comments,
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Result.Failure<GetMovieCommentsResponse>("Failed to fetch movie genres");
        }
    }
}