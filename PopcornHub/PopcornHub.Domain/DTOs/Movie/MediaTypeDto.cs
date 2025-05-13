using TMDbLib.Objects.General;

namespace PopcornHub.Domain.DTOs.Movie;

public class MediaTypeDto 
{
    public MediaTypeDto(MediaType mediaType)
    {
        Name = mediaType.ToString();
        Value = (int)mediaType;
    }
    
    public string Name { get; init; }
    
    public int Value { get; init; }
}