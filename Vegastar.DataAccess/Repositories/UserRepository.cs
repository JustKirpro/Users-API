using System.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Vegastar.DataAccess.Options;
using Vegastar.Domain.Contracts;
using Vegastar.Domain.Entities;
using Vegastar.Domain.Exceptions;

using UserGroupCode = Vegastar.DataAccess.Entities.UserGroupCode;
using UserStateCode = Vegastar.DataAccess.Entities.UserStateCode;

namespace Vegastar.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UsersContext _usersContext;
    private readonly IMapper _mapper;
    private readonly RepositoryOptions _options;

    public UserRepository(UsersContext usersContext, IMapper mapper, IOptions<RepositoryOptions> options)
    {
        _usersContext = usersContext;
        _mapper = mapper;
        _options = options.Value;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await using var transaction = await _usersContext.Database.BeginTransactionAsync(IsolationLevel.Serializable);

        var userToAdd = await GetUserToAdd(user);
        await EnsureUserCanBeAdded(userToAdd);
        
        await _usersContext.Users.AddAsync(userToAdd);
        await Task.Delay(TimeSpan.FromSeconds(_options.UserCreationTimeInSeconds));

        await _usersContext.SaveChangesAsync();
        await transaction.CommitAsync();

        return _mapper.Map<User>(userToAdd);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync() => await _usersContext.Users
        .Include(u => u.Group)
        .Include(u => u.State)
        .Where(u => u.State.Code == UserStateCode.Active)
        .Select(u => _mapper.Map<User>(u))
        .ToListAsync();

    public async Task<User?> GetUserByIdAsync(long userId)
    {
        var foundUser = await GetUserById(userId);
        return foundUser is null ? default : _mapper.Map<User>(foundUser);
    }

    public async Task<User?> RemoveUserByIdAsync(long userId)
    {
        await using var transaction = await _usersContext.Database.BeginTransactionAsync();

        var foundUser = await GetUserById(userId);

        if (foundUser is null)
        {
            return default;
        }

        var blockedState = await _usersContext.UserStates.SingleAsync(s => s.Code == UserStateCode.Blocked);
        foundUser.State = blockedState;

        await _usersContext.SaveChangesAsync();
        await transaction.CommitAsync();

        return _mapper.Map<User>(foundUser);
    }

    private async Task<Entities.User> GetUserToAdd(User user)
    {
        var userToAdd = _mapper.Map<Entities.User>(user);

        userToAdd.Group = await _usersContext.UserGroups.SingleAsync(g => g.Code == userToAdd.Group.Code);
        userToAdd.State = await _usersContext.UserStates.SingleAsync(s => s.Code == UserStateCode.Active);
        userToAdd.CreatedDate = DateTimeOffset.UtcNow;

        return userToAdd;
    }

    private async Task<Entities.User?> GetUserById(long userId) => await _usersContext.Users
        .Include(u => u.State)
        .Include(u => u.Group)
        .SingleOrDefaultAsync(u => u.UserId == userId && u.State.Code == UserStateCode.Active);

    private async Task EnsureUserCanBeAdded(Entities.User user)
    {
        if (await GetUserByLogin(user.Login) is not null)
        {
            throw new LoginTakenException($"User with login '{user.Login}' already exists.");
        }

        if (user.Group.Code == UserGroupCode.Admin && await IsAdminCreated())
        {
            throw new AdminAlreadyExistsException("Admin already exists.");
        }
    }

    private async Task<Entities.User?> GetUserByLogin(string login) => await _usersContext.Users
        .SingleOrDefaultAsync(u => u.Login == login && u.State.Code == UserStateCode.Active);

    private async Task<bool> IsAdminCreated()
    {
        var admin = await _usersContext.Users.SingleOrDefaultAsync(u => u.Group.Code == UserGroupCode.Admin);
        return admin is not null;
    }
}