namespace PopcornHub.Domain.ValueObjects;

public class MovieId 
{
    public MovieId() {}
    
    public MovieId(int value)
    {
        Value = value;
    }

    public int Value { get; }
}