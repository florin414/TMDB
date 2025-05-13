using PopcornHub.Domain.DTOs.Movie;
using PopcornHub.Domain.ValueObjects;

namespace PopcornHub.Domain.Models.Movie;

public class MovieCreditsModel
{
    public MovieId MovieId { get; init; }
    
    public CreditsDto Credits { get; set; }
}