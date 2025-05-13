using PopcornHub.Domain;
using PopcornHub.Domain.IServices;
using PopcornHub.Domain.ValueObjects;

namespace PopcornHub.Infrastructure.DomainServices;

public class TmdbMovieExistenceChecker : IMovieExistenceChecker
{
    private readonly IMovieApiClient _client;

    public TmdbMovieExistenceChecker(IMovieApiClient client)
    {
        _client = client;
    }

    public async Task<bool> ExistsAsync(MovieId id)
    {
        var movie = await _client.GetMovieAsync(id.Value);
        return movie != null;
    }
}