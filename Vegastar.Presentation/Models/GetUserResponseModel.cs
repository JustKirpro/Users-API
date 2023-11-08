using Vegastar.Domain.Entities;

namespace Vegastar.Presentation.Models;

public record GetUserResponseModel
{
    public string Login { get; init; } = null!;

    public UserGroupCode GroupCode { get; init; }
    
    public UserStateCode StateCode { get; init; }
}