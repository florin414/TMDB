namespace PopcornHub.Domain.Models.Movie;

public class MoviesModel
{

    public string? Name { get; init; }
    

    public string? Genre { get; init; }


    public string? SortBy { get; init; }
    

    public int Page { get; init; }
    

    public int PageSize { get; init; }
}