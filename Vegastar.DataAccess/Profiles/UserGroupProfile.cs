using AutoMapper;
using Vegastar.DataAccess.Entities;

namespace Vegastar.DataAccess.Profiles;

public class UserGroupProfile : Profile
{
    public UserGroupProfile()
    {
        CreateMap<UserGroup, Domain.Entities.UserGroup>()
            .ReverseMap();
    }
}