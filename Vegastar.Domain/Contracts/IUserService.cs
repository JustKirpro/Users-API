using Vegastar.Domain.Entities;

namespace Vegastar.Domain.Contracts;

public interface IUserService
{
    public Task<User> AddUserAsync(User user);

    public Task<IEnumerable<User>> GetUsersAsync();
    
    public Task<User> GetUserByIdAsync(long userId);
    
    public Task RemoveUserById(long userId);
}