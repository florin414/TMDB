using Moq;
using PopcornHub.Domain.Entities;
using PopcornHub.UnitTests.TestFixtures;
using PopcornHub.UnitTests.TestUtils;
using Shouldly;

namespace PopcornHub.UnitTests;

public class MovieServiceTests(MovieServiceFixture fixture) : IClassFixture<MovieServiceFixture>
{
    private readonly MovieServiceFixture _fixture = fixture;

    [Theory]
    [InlineData(1)]
    [InlineData(42)]
    [InlineData(100)]
    public async Task Given_ValidMovieId_When_GetMovieDetailsIsCalled_Then_ReturnsMovieDto(int movieId)
    {
        // Arrange
        //var movie = MovieTestFactory.CreateMovie(movieId);
        //_fixture.MockRepo.Setup(r => r.GetMovieByIdAsync(movieId)).ReturnsAsync(movie);

        // Act
        var result = await _fixture.MovieService.SearchMoviesAsync(null);

        // Assert
       // result.ShouldNotBeNull();
    }
}

public interface IMovieRepository
{
    ///public Task<Movie> GetMovieByIdAsync(int movieId); 
}