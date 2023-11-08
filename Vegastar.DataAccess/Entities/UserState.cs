namespace Vegastar.DataAccess.Entities;

public class UserState
{
    public long UserStateId { get; init; }
    
    public UserStateCode Code { get; init; }

    public string? Description { get; init; }

    public ICollection<User> Users { get; init; } = null!;
}

public enum UserStateCode
{
    Active = 1,
    Blocked
}