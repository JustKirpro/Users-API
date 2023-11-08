namespace Vegastar.Domain.Entities;

public record UserState
{
    public long UserStateId { get; init; }
    
    public UserStateCode Code { get; init; }

    public string Description { get; init; } = string.Empty;
}

public enum UserStateCode
{
    Active = 1,
    Blocked
}