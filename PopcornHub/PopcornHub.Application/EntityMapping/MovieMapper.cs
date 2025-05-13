using PopcornHub.Domain.DTOs.Movie;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace PopcornHub.Application.EntityMapping;

public static class MovieMapper
{
    public static MovieDto ToDto(this Movie movie)
        => new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            PosterPath = movie.PosterPath,
            Overview = movie.Overview,
            ReleaseDate = movie.ReleaseDate,
            VoteAverage = movie.VoteAverage,
        };
    
    public static MovieDto ToDto(this SearchMovie movie)
        => new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            PosterPath = movie.PosterPath,
            Overview = movie.Overview,
            ReleaseDate = movie.ReleaseDate,
            VoteAverage = movie.VoteAverage,
        };

    public static List<MovieDto> ToDtoList(this List<SearchMovie> movies)
        => movies.Select(m => m.ToDto()).ToList();
    
    public static List<MovieDto> ToDtoList(this SearchContainer<SearchMovie> movies)
        => movies.Results.Select(m => m.ToDto()).ToList();
    
}