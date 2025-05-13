using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PopcornHub.Domain.IRepositories;
using PopcornHub.Infrastructure.Context;

namespace PopcornHub.Infrastructure.Repositories;

public class EfCoreRepository : ReadOnlyEfCoreRepository, IEfCoreRepository
{
    private readonly PopcornHubDbContext _context;

    public EfCoreRepository(PopcornHubDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken); 
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync();  
    }
}
