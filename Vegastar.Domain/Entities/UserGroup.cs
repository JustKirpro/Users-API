namespace Vegastar.Domain.Entities;

public record UserGroup
{
    public long UserGroupId { get; init; }
    
    public UserGroupCode Code { get; init; }

    public string Description { get; init; } = string.Empty;
}

public enum UserGroupCode
{
    Admin = 1,
    User
}