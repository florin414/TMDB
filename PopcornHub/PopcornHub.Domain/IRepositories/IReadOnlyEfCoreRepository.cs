namespace PopcornHub.Domain.IRepositories;

public interface IReadOnlyEfCoreRepository
{
    Task<IQueryable<TEntity>> GetQueryableAsync<TEntity>() where TEntity : class;
}
