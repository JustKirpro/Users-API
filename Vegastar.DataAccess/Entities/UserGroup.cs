namespace Vegastar.DataAccess.Entities;

public class UserGroup
{
    public long UserGroupId { get; init; }
    
    public UserGroupCode Code { get; init; }

    public string? Description { get; init; }

    public ICollection<User> Users { get; init; } = null!;
}

public enum UserGroupCode
{
    Admin = 1,
    User
}