using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PopcornHub.Application.EntityMapping;
using PopcornHub.Application.IServices;
using PopcornHub.Domain.Entities;
using PopcornHub.Domain.IRepositories;
using PopcornHub.Domain.IServices;
using PopcornHub.Domain.Models.Movie;
using PopcornHub.Shared.Constants;

namespace PopcornHub.Application.Services;

public class CommentService : ICommentService
{
    private readonly IRepositoryProvider _repositoryProvider;
    private readonly ILogger<CommentService> _logger;
    private readonly IMovieExistenceChecker _movieExistenceChecker;
    private readonly ICurrentUserService _currentUserService;
    
    public CommentService(
        IRepositoryProvider repositoryProvider, 
        ILogger<CommentService> logger, 
        IMovieExistenceChecker movieExistenceChecker, ICurrentUserService currentUserService)
    {
        _repositoryProvider = repositoryProvider;
        _logger = logger;
        _movieExistenceChecker = movieExistenceChecker;
        _currentUserService = currentUserService;
    }

    public async Task<Result<MovieCommentModel>> CreateMovieCommentAsync(MovieCommentModel model)
    {
        try
        {
            var movieExists = await _movieExistenceChecker.ExistsAsync(model.MovieId);
            if (!movieExists)
            {
                return Result.Failure<MovieCommentModel>(MovieFailureCodes.MovieNotFound); 
            }

            var comment = new MovieComment(model.MovieId, model.Comment, _currentUserService.GetUserId());
            var repository = _repositoryProvider.GetRepository();
            await repository.InsertAsync(comment);
            await repository.SaveChangesAsync();
            
            return Result.Success(model);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while trying to create a comment for MovieId={MovieId}. Error: {Message}", model.MovieId, e.Message);
            
            return Result.Failure<MovieCommentModel>(MovieFailureCodes.FailedToCreateComment);
        }
    }

    public async Task<Result<GetMovieCommentsModel>> GetMovieCommentsAsync(MovieCommentsModel model)
    {
        try
        {
            var movieExists = await _movieExistenceChecker.ExistsAsync(model.MovieId);
            if (!movieExists)
            {
                return Result.Failure<GetMovieCommentsModel>(MovieFailureCodes.MovieNotFound);
            }

            var repo = _repositoryProvider.GetReadOnlyRepository();
            var query = await repo.GetQueryableAsync<MovieComment>();

            if (!model.Cursor.HasValue)
            {
                model.Cursor = query.OrderBy(x => x.Id).LastOrDefault().Id;
            }
            
            var comments = query
                .Where(c => c.MovieId.Value == model.MovieId.Value)
                .Where(c => c.Id < model.Cursor.Value) 
                .OrderByDescending(c => c.CreatedAt)
                .Take(model.Limit + 1)
                .ToList();

            var hasNext = comments.Count > model.Limit;
            var page = comments.Take(model.Limit).ToList();
            var nextCursor = hasNext ? page.Last().Id : (int?)null;

            return Result.Success(new GetMovieCommentsModel
            {
                MovieComments = page.Select(x => x.ToModel()).ToList(),
                NextCursor = nextCursor
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An error occurred while fetching paginated comments for movie with ID: {MovieId}. Error: {Message}",
                model.MovieId, ex.Message);

            return Result.Failure<GetMovieCommentsModel>(MovieFailureCodes.FailedToFetchComments);
        }
    }
}
