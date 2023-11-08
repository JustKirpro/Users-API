namespace Vegastar.DataAccess.Entities;

public class User
{
    public long UserId { get; set; }

    public string Login { get; init; } = null!;

    public string Password { get; init; } = null!;
    
    public DateTimeOffset CreatedDate { get; set; }
    
    public long GroupId { get; set; }

    public UserGroup Group { get; set; } = null!;
    
    public long StateId { get; set; }

    public UserState State { get; set; } = null!;
}