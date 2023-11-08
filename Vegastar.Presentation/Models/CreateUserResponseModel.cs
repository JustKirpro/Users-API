using Vegastar.Domain.Entities;

namespace Vegastar.Presentation.Models;

public record CreateUserResponseModel
{
    public long UserId { get; init; }

    public string Login { get; init; } = null!;
    
    public UserGroupCode GroupCode { get; init; }
    
    public UserStateCode StateCode {get; init; }
}