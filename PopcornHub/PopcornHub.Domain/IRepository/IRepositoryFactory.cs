using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace PopcornHub.Domain.IRepository;

public interface IRepositoryFactory
{
    IBasicRepository<TEntity, int> GetBasicRepository<TEntity>()
        where TEntity : class, IEntity<int>;

    IReadOnlyRepository<TEntity, int> GetReadOnlyRepository<TEntity>()
        where TEntity : class, IEntity<int>;
}