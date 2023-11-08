using Vegastar.Domain.Entities;

namespace Vegastar.TestingInfrastructure.Extensions;

public static class UserExtensions
{
    public static User WithLogin(this User user, string login) => user with
    {
        Login = login
    };

    public static User WithGroupCode(this User user, UserGroupCode code) => user with
    {
        Group = new UserGroup
        {
            Code = code
        }
    };
}