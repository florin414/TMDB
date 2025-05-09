using TMDbLib.Objects.General;
using Volo.Abp.Application.Dtos;

namespace PopcornHub.Domain.DTOs.Movies;

public class MovieGenresResponse : EntityDto
{
    public List<Genre> Genres  { get; set; }
}