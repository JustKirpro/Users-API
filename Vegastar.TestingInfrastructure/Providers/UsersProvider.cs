using Vegastar.Domain.Entities;

namespace Vegastar.TestingInfrastructure.Providers;

public static class UsersProvider
{
    public static User GetFirstUser()
    {
        return new User
        {
            Login = "First",
            Password = "First",
            Group = new UserGroup
            {
                Code = UserGroupCode.User
            }
        };
    }
    
    public static User GetSecondUser()
    {
        return new User
        {
            Login = "Second",
            Password = "Second",
            Group = new UserGroup
            {
                Code = UserGroupCode.User
            }
        };
    }
}