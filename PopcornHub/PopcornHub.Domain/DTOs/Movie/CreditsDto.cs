namespace PopcornHub.Domain.DTOs.Movie;

public class CreditsDto 
{
    public int MovieId { get; init; }
    
    public List<CastDto> Cast { get; init; } = [];
}

