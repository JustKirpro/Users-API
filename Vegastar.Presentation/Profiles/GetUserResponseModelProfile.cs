using AutoMapper;
using Vegastar.Domain.Entities;
using Vegastar.Presentation.Models;

namespace Vegastar.Presentation.Profiles;

public class GetUserResponseModelProfile : Profile
{
    public GetUserResponseModelProfile()
    {
        CreateMap<User, GetUserResponseModel>()
            .ForMember(m => m.GroupCode, o =>
            {
                o.MapFrom(u => u.Group.Code);
            })
            .ForMember(m => m.StateCode, o =>
            {
                o.MapFrom(u => u.State.Code);
            });
    }
}