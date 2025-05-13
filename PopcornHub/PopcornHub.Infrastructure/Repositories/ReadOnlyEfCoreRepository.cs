using Microsoft.EntityFrameworkCore;
using PopcornHub.Domain.IRepositories;
using PopcornHub.Infrastructure.Context;

namespace PopcornHub.Infrastructure.Repositories;

public class ReadOnlyEfCoreRepository : IReadOnlyEfCoreRepository
{
    private readonly PopcornHubDbContext _context;

    public ReadOnlyEfCoreRepository(PopcornHubDbContext context)
    {
        _context = context;
    }
    
    public Task<IQueryable<TEntity>> GetQueryableAsync<TEntity>() where TEntity : class
    {
        return Task.FromResult(_context.Set<TEntity>().AsNoTracking());
    }
}