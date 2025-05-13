using System.Linq.Expressions;

namespace PopcornHub.Domain.IRepositories;

public interface IEfCoreRepository: IReadOnlyEfCoreRepository
{
    Task InsertAsync<TEntity> (TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}


