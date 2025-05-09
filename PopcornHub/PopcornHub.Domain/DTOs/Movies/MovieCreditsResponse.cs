using TMDbLib.Objects.Movies;
using Volo.Abp.Application.Dtos;

namespace PopcornHub.Domain.DTOs.Movies;

public class MovieCreditsResponse : EntityDto
{
    public Credits Credits { get; set; }
}