using Vegastar.Domain.Contracts;
using Vegastar.Domain.Services;

namespace Vegastar.TestingInfrastructure.Providers;

public static class UserServiceProvider
{
    public static UserService GetUserService(IUserRepository repository) => new(new PasswordHasherService(), repository);
}