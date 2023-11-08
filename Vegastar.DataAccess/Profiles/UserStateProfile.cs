using AutoMapper;
using Vegastar.DataAccess.Entities;

namespace Vegastar.DataAccess.Profiles;

public class UserStateProfile : Profile
{
    public UserStateProfile()
    {
        CreateMap<UserState, Domain.Entities.UserState>()
            .ReverseMap();
    }
}