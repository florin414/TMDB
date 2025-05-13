namespace PopcornHub.Domain.IRepositories;

public interface IRepositoryProvider
{
    IEfCoreRepository GetRepository();

    IReadOnlyEfCoreRepository GetReadOnlyRepository();
}


