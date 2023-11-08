using Vegastar.Domain.Contracts;
using Vegastar.Domain.Entities;
using Vegastar.Domain.Exceptions;

namespace Vegastar.Domain.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) => _userRepository = userRepository;

    public async Task<User> AddUserAsync(User user)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
        var userWithHashesPassword = user with
        {
            Password = hashedPassword
        };
        
        var addedUser = await _userRepository.CreateUserAsync(userWithHashesPassword);
        return addedUser;
    }
    
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        var foundUsers = await _userRepository.GetAllUsersAsync();
        return foundUsers;
    }
    
    public async Task<User> GetUserByIdAsync(long userId)
    {
        var foundUser = await _userRepository.GetUserByIdAsync(userId);
        EnsureUserNotNull(foundUser, userId);
        
        return foundUser!;
    }
    
    public async Task RemoveUserById(long userId)
    {
        var removedUser = await _userRepository.RemoveUserByIdAsync(userId);
        EnsureUserNotNull(removedUser, userId);
    }
    
    private static void EnsureUserNotNull(User? user, long userId)
    {
        if (user is null)
        {
            throw new UserNotFoundException($"User with id {userId} was not found.");
        }
    }
}