namespace PopcornHub.Domain.ValueObjects;

public class Username
{
    public Username() {}
    
    public Username(string value)
    {
        Value = value;
    }

    public string Value { get; }
}