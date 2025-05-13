using PopcornHub.Domain.IRepositories;

namespace PopcornHub.Infrastructure.Repositories;

public class RepositoryProvider : IRepositoryProvider
{
    private readonly IServiceProvider _serviceProvider;
    
    public RepositoryProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public IReadOnlyEfCoreRepository GetReadOnlyRepository()
    {
        var repository = _serviceProvider.GetService(typeof(IReadOnlyEfCoreRepository));

        return (IReadOnlyEfCoreRepository)repository;
    }

    public IEfCoreRepository GetRepository()
    {
        var repository = _serviceProvider.GetService(typeof(IEfCoreRepository));
        
        return (IEfCoreRepository)repository;
    }
}