using PopcornHub.Domain.IRepository;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace PopcornHub.Infrastructure.Repository;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;
    
    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IBasicRepository<TEntity, int> GetBasicRepository<TEntity>() where TEntity : class, IEntity<int>
    {
        var repositoryType = typeof(IBasicRepository<,>).MakeGenericType(typeof(TEntity), typeof(int));
        
        var repository = _serviceProvider.GetService(repositoryType);
        
        return (IBasicRepository<TEntity, int>)repository;
    }

    public IReadOnlyRepository<TEntity, int> GetReadOnlyRepository<TEntity>() where TEntity : class, IEntity<int> 
    {
        var repositoryType = typeof(IReadOnlyRepository<,>).MakeGenericType(typeof(TEntity), typeof(int));
        
        var repository = _serviceProvider.GetService(repositoryType);
        
        return (IReadOnlyRepository<TEntity, int>)repository;
    }
}