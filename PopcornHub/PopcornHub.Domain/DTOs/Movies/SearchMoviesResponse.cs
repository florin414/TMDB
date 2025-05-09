using TMDbLib.Objects.Search;
using Volo.Abp.Application.Dtos;

namespace PopcornHub.Domain.DTOs.Movies;

// refactor
public class SearchMoviesResponse : EntityDto
{
    public List<SearchMovie> Movies { get; set; } = [];
}