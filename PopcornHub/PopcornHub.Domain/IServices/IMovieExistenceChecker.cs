using PopcornHub.Domain.ValueObjects;

namespace PopcornHub.Domain.IServices;

public interface IMovieExistenceChecker : IDomainService
{
    Task<bool> ExistsAsync(MovieId id);
}