using Vegastar.Domain.Entities;

namespace Vegastar.Domain.Contracts;

public interface IUserRepository
{
    public Task<User> CreateUserAsync(User user);

    public Task<IEnumerable<User>> GetAllUsersAsync();

    public Task<User?> GetUserByIdAsync(long userId);

    public Task<User?> RemoveUserByIdAsync(long userId);
}