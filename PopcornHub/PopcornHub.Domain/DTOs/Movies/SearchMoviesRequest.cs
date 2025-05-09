using FastEndpoints;
using Volo.Abp.Application.Dtos;

namespace PopcornHub.Domain.DTOs.Movies;

public class SearchMoviesRequest : EntityDto
{
    [QueryParam]
    public string? Name { get; set; }
    
    [QueryParam]
    public string? Genre { get; set; }

    [QueryParam] 
    public bool Top { get; set; } = false;
    
    [QueryParam] 
    public bool Latest { get; set; } = false;
}