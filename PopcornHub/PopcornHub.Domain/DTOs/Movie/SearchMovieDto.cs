namespace PopcornHub.Domain.DTOs.Movie;

public class SearchMovieDto 
{
    public int Id { get; init; }
    
    public MediaTypeDto MediaType { get; init; }
    
    public double Popularity { get; init; }

    public string BackdropPath { get; init; }
    
    public List<int> GenreIds { get; init; }
    
    public string OriginalLanguage { get; init; }
    
    public string Overview { get; init; }
    
    public string PosterPath { get; init; }
    
    public double VoteAverage { get; init; }
    
    public int VoteCount { get; init; }

    public bool Adult { get; init; }
    
    public string OriginalTitle { get; init; }
    
    public DateTime? ReleaseDate { get; init; }
    
    public string Title { get; init; }
    
    public bool Video { get; init; }
}