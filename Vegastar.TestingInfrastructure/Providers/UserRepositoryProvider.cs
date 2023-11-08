using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Vegastar.DataAccess;
using Vegastar.DataAccess.Options;
using Vegastar.DataAccess.Profiles;
using Vegastar.DataAccess.Repositories;

namespace Vegastar.TestingInfrastructure.Providers;

public static class UserRepositoryProvider
{
    public static UserRepository GetUserRepository(int userCreationTimeInSeconds = 0)
    {
        var usersContext = CreateInMemoryUsersContext();
        var mapper = CreateIMapperProfile();
        var cacheOptions = CreateCacheOptions(userCreationTimeInSeconds);

        return new UserRepository(usersContext, mapper, cacheOptions);
    }

    private static UsersContext CreateInMemoryUsersContext()
    {
        var contextOptions = new DbContextOptionsBuilder<UsersContext>()
            .UseInMemoryDatabase("VegastarTestingDatabase")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        var usersContext = new UsersContext(contextOptions);
        usersContext.Database.EnsureDeleted();
        usersContext.Database.EnsureCreated();

        return usersContext;
    }

    private static IMapper CreateIMapperProfile()
    {
        var mapperConfiguration = new MapperConfiguration(config =>
        {
            config.AddProfile(new UserProfile());
            config.AddProfile(new UserGroupProfile());
            config.AddProfile(new UserStateProfile());
        });

        return mapperConfiguration.CreateMapper();
    }

    private static IOptions<RepositoryOptions> CreateCacheOptions(int userCreationTimeInSeconds)
    {
        var cachingOptions = new RepositoryOptions
        {
            UserCreationTimeInSeconds = userCreationTimeInSeconds
        };

        return Options.Create(cachingOptions);
    }
}