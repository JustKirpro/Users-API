using AutoMapper;
using Vegastar.DataAccess.Entities;

namespace Vegastar.DataAccess.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, Domain.Entities.User>()
            .ReverseMap();
    }
}