using FluentAssertions;
using Vegastar.Domain.Entities;
using Vegastar.Domain.Exceptions;
using Vegastar.TestingInfrastructure.Extensions;
using Vegastar.TestingInfrastructure.Providers;

namespace Vegastar.UnitTests;

public class UserRepositoryTests
{
    [Fact]
    public async Task CreateUserAsync_ValidUser_User()
    {
        // Arrange
        const int minIdValue = 1;
        var repository = UserRepositoryProvider.GetUserRepository();
        
        var expectedUser = UsersProvider.GetFirstUser();
        
        // Act
        var actualUser = await repository.CreateUserAsync(expectedUser);
        
        // Asserts
        actualUser.Login.Should().BeEquivalentTo(expectedUser.Login);
        actualUser.Password.Should().BeEquivalentTo(expectedUser.Password);
        actualUser.Group.Code.Should().Be(expectedUser.Group.Code);

        actualUser.UserId.Should().BeGreaterOrEqualTo(minIdValue);
        actualUser.CreatedDate.Should().NotBe(default);
        actualUser.State.Code.Should().Be(UserStateCode.Active);
    }

    [Fact]
    public async Task CreateUserAsync_RepetitiveLogin_LoginTakenException()
    {
        // Arrange
        var repository = UserRepositoryProvider.GetUserRepository();
        
        var firstUser = UsersProvider.GetFirstUser();
        await repository.CreateUserAsync(firstUser);

        var secondUser = UsersProvider.GetSecondUser().WithLogin(firstUser.Login);
        
        // Act
        var act = async () => await repository.CreateUserAsync(secondUser);

        // Asserts
        await act.Should().ThrowAsync<LoginTakenException>();
    }
    
    [Fact]
    public async Task CreateUserAsync_SecondAdmin_AdminAlreadyExistsExceptions()
    {
        // Arrange
        var repository = UserRepositoryProvider.GetUserRepository();

        var firstUser = UsersProvider.GetFirstUser().WithGroupCode(UserGroupCode.Admin);
        await repository.CreateUserAsync(firstUser);

        var secondUser = UsersProvider.GetSecondUser().WithGroupCode(UserGroupCode.Admin);
        
        // Act
        var act = async () => await repository.CreateUserAsync(secondUser);

        // Asserts
        await act.Should().ThrowAsync<AdminAlreadyExistsException>();
    }

    [Fact]
    public async Task GetAllUsersAsync_EmptyDatabase_NoUsers()
    {
        // Arrange
        const int expectedCount = 0;
        var repository = UserRepositoryProvider.GetUserRepository();
        
        // Act
        var actualUsers = await repository.GetAllUsersAsync(); 

        // Asserts
        actualUsers.Should().HaveCount(expectedCount);
    }
    
    [Fact]
    public async Task GetAllUsersAsync_SeveralUsers_AllUsers()
    {
        // Arrange
        const int expectedCount = 2;
        var repository = UserRepositoryProvider.GetUserRepository();

        var firstUser = UsersProvider.GetFirstUser();
        await repository.CreateUserAsync(firstUser);

        var secondUser = UsersProvider.GetSecondUser();
        await repository.CreateUserAsync(secondUser);
        
        // Act
        var actualUsers = await repository.GetAllUsersAsync(); 

        // Asserts
        actualUsers.Should().HaveCount(expectedCount);
    }

    [Fact]
    public async Task GetUserByIdAsync_ExistingUser_User()
    {
        // Arrange
        var repository = UserRepositoryProvider.GetUserRepository();

        var user = UsersProvider.GetFirstUser();
        var expectedUser = await repository.CreateUserAsync(user);
        
        // Act
        var actualUser = await repository.GetUserByIdAsync(expectedUser.UserId); 

        // Asserts
        actualUser.Should().BeEquivalentTo(expectedUser);
    }
    
    [Fact]
    public async Task GetUserByIdAsync_NonExistingUser_Null()
    {
        // Arrange
        const int userId = 1;
        var repository = UserRepositoryProvider.GetUserRepository();
        
        // Act
        var actualUser = await repository.GetUserByIdAsync(userId); 

        // Asserts
        actualUser.Should().BeNull();
    }
    
    [Fact]
    public async Task RemoveUserByIdAsync_ExistingUser_User()
    {
        // Arrange
        var repository = UserRepositoryProvider.GetUserRepository();

        var user = UsersProvider.GetFirstUser();
        var expectedUser = await repository.CreateUserAsync(user);
        
        // Act
        var actualUser = await repository.RemoveUserByIdAsync(expectedUser.UserId); 

        // Asserts
        actualUser.Should().BeEquivalentTo(expectedUser, o => o.Excluding(u => u.State));
        actualUser!.State.Code.Should().Be(UserStateCode.Blocked);
    }
    
    [Fact]
    public async Task RemoveUserByIdAsync_NonExistingUser_Null()
    {
        // Arrange
        const int userId = 1;
        var repository = UserRepositoryProvider.GetUserRepository();
        
        // Act
        var actualUser = await repository.RemoveUserByIdAsync(userId); 

        // Asserts
        actualUser.Should().BeNull();
    }
}