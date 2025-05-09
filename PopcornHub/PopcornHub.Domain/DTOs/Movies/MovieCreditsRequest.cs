using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace PopcornHub.Domain.DTOs.Movies;

public class MovieCreditsRequest : EntityDto
{
    [Required]
    public int MovieId { get; set; }
}