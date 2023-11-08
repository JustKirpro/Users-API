using Microsoft.EntityFrameworkCore;
using Vegastar.DataAccess.Entities;

namespace Vegastar.DataAccess;

public sealed class UsersContext : DbContext
{
    public DbSet<User> Users { get; init; } = null!;

    public DbSet<UserGroup> UserGroups { get; init; } = null!;

    public DbSet<UserState> UserStates { get; init; } = null!;

    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Login);
        
        SeedUserGroups(modelBuilder);
        SeedUserStates(modelBuilder);
        
        base.OnModelCreating(modelBuilder);
    }

    private static void SeedUserGroups(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserGroup>().HasData(new[]
        {
            new UserGroup
            {
                UserGroupId = (int) UserGroupCode.Admin,
                Code = UserGroupCode.Admin,
                Description = "Admin"
            },
            new UserGroup
            {
                UserGroupId = (int) UserGroupCode.User,
                Code = UserGroupCode.User,
                Description = "Regular user"
            }
        });
    }

    private static void SeedUserStates(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserState>().HasData(new[]
        {
            new UserState
            {
                UserStateId = (int) UserStateCode.Active,
                Code = UserStateCode.Active,
                Description = "Active"
            },
            new UserState
            {
                UserStateId = (int) UserStateCode.Blocked,
                Code = UserStateCode.Blocked,
                Description = "Blocked"
            }
        });
    }
}