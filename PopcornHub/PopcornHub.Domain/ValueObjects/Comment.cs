namespace PopcornHub.Domain.ValueObjects;

public class Comment 
{
    public Comment() {}
    
    public Comment(string value)
    {
        Value = value;
    }

    public string Value { get; }
}