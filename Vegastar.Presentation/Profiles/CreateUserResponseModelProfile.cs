using AutoMapper;
using Vegastar.Domain.Entities;
using Vegastar.Presentation.Models;

namespace Vegastar.Presentation.Profiles;

public class CreateUserResponseModelProfile : Profile
{
    public CreateUserResponseModelProfile()
    {
        CreateMap<User, CreateUserResponseModel>()
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