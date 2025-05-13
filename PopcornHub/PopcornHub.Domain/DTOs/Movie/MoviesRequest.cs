using FastEndpoints;

namespace PopcornHub.Domain.DTOs.Movie;

public class MoviesRequest 
{
    [QueryParam]
    public string? Name { get; init; }
    
    [QueryParam]
    public string? Genre { get; init; }

    [QueryParam] 
    public string? SortBy { get; init; }
    
    [QueryParam] 
    public int Page { get; init; }
    
    [QueryParam] 
    public int PageSize { get; init; }
}