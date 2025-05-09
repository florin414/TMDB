using Moq;
using PopcornHub.Application.Services;

namespace PopcornHub.UnitTests.TestFixtures;

public class MovieServiceFixture : IDisposable
{
    // Mock-ul repository-ului
    public Mock<IMovieRepository> MockRepo { get; }
    public MovieService MovieService { get; }

    // Constructor care se execută pentru fiecare test
    public MovieServiceFixture()
    {
        // Setup-ul global pentru testele din clasă
        MockRepo = new Mock<IMovieRepository>();
        //MovieService = new MovieService(MockRepo.Object);
        MovieService = new MovieService(null, null);
    }

    public void Dispose()
    {
        
    }
}