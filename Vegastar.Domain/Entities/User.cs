namespace Vegastar.Domain.Entities;

public record User
{
    public long UserId { get; init; }

    public string Login { get; init; } = null!;

    public string Password { get; init; } = null!;
    
    public DateTimeOffset CreatedDate { get; init; }

    public UserGroup Group { get; init; } = null!;

    public UserState State { get; init; } = null!;
}