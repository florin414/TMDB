using Microsoft.Extensions.Logging;
using Moq;
using PopcornHub.Application.IServices;
using PopcornHub.Application.Services;
using PopcornHub.Domain.IRepositories;
using PopcornHub.Domain.IServices;
using PopcornHub.UnitTests.TestUtils;

public class CommentServiceFixture
{
    public Mock<IRepositoryProvider> RepositoryProviderMock { get; }
    public Mock<IEfCoreRepository> EfCoreRepositoryMock { get; }
    public Mock<IReadOnlyEfCoreRepository> ReadOnlyRepositoryMock { get; }
    public Mock<IMovieExistenceChecker> MovieExistenceCheckerMock { get; }
    public Mock<ICurrentUserService> CurrentUserServiceMock { get; }
    public Mock<ILogger<CommentService>> LoggerMock { get; }

    public CommentService Service { get; }

    public CommentServiceFixture()
    {
        RepositoryProviderMock = new Mock<IRepositoryProvider>();
        EfCoreRepositoryMock = new Mock<IEfCoreRepository>();
        ReadOnlyRepositoryMock = new Mock<IReadOnlyEfCoreRepository>();
        MovieExistenceCheckerMock = new Mock<IMovieExistenceChecker>();
        CurrentUserServiceMock = new Mock<ICurrentUserService>();
        LoggerMock = new Mock<ILogger<CommentService>>();

        RepositoryProviderMock
            .Setup(r => r.GetRepository())
            .Returns(EfCoreRepositoryMock.Object);

        RepositoryProviderMock
            .Setup(r => r.GetReadOnlyRepository())
            .Returns(ReadOnlyRepositoryMock.Object);

        CurrentUserServiceMock
            .Setup(x => x.GetUserId())
            .Returns(UserRepositoryMock.UserId);

        Service = new CommentService(
            RepositoryProviderMock.Object,
            LoggerMock.Object,
            MovieExistenceCheckerMock.Object,
            CurrentUserServiceMock.Object
        );
    }
}