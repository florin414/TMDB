namespace PopcornHub.Domain.DTOs.Movie;

public class MovieDto
{
    public int Id { get; init; }
    
    public string Title { get; init; }
    
    public string PosterPath { get; init; }
    
    public string Overview { get; init; }
    
    public DateTime? ReleaseDate { get; init; }
    
    public double VoteAverage { get; init; }
}