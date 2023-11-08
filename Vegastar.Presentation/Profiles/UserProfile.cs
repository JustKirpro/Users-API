using AutoMapper;
using Vegastar.Domain.Entities;
using Vegastar.Presentation.Models;

namespace Vegastar.Presentation.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserRequestModel, User>()
            .ForMember(u => u.Group, o =>
            {
                o.MapFrom(m => new UserGroup
                {
                    Code = m.UserGroupCode
                });
            });
    }
}