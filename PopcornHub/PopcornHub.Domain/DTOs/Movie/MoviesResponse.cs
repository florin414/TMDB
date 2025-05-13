namespace PopcornHub.Domain.DTOs.Movie;

public class MoviesResponse 
{
    public List<MovieDto> Movies { get; init; } = [];
    
    public int TotalCount { get; init; }
    
    public int Page { get; init; }
    
    public int PageSize { get; init; }
}