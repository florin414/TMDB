using Moq;
using PopcornHub.Domain.Entities;
using PopcornHub.Shared.Constants;
using PopcornHub.UnitTests.TestUtils;
using Shouldly;

namespace PopcornHub.UnitTests.CommentServiceTests;

public class CommentServiceTests : IClassFixture<CommentServiceFixture>
{
    private readonly CommentServiceFixture _fixture;

    public CommentServiceTests(CommentServiceFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GivenExistingMovieId_WhenCreateComment_ThenReturnsSuccess()
    {
        var model = MovieCommentModelFactory.CreateValid();

        _fixture.MovieExistenceCheckerMock
            .Setup(x => x.ExistsAsync(model.MovieId))
            .ReturnsAsync(true);
        
        var result = await _fixture.Service.CreateMovieCommentAsync(model);
        
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe(model);

        _fixture.EfCoreRepositoryMock.Verify(x => x.InsertAsync(It.Is<MovieComment>(c =>
                c.MovieId == model.MovieId &&
                c.Comment == model.Comment &&
                c.UserId ==  UserRepositoryMock.UserId
        ), It.IsAny<CancellationToken>()), Times.Once);
        

        _fixture.EfCoreRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GivenInvalidMovieId_WhenCreateComment_ThenReturnsMovieNotFoundFailure()
    {
        var model = MovieCommentModelFactory.CreateInvalid();

        _fixture.MovieExistenceCheckerMock
            .Setup(x => x.ExistsAsync(model.MovieId))
            .ReturnsAsync(false);
        
        var result = await _fixture.Service.CreateMovieCommentAsync(model);
        
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(MovieFailureCodes.MovieNotFound);

        _fixture.EfCoreRepositoryMock.Verify(x => x.InsertAsync(It.IsAny<MovieComment>(), It.IsAny<CancellationToken>()), Times.Never);
        
        _fixture.EfCoreRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
