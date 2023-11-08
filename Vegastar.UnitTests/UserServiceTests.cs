using FluentAssertions;
using Moq;
using Vegastar.Domain.Contracts;
using Vegastar.Domain.Entities;
using Vegastar.Domain.Exceptions;
using Vegastar.TestingInfrastructure.Providers;

namespace Vegastar.UnitTests;

public class UserServiceTests
{
    [Fact]
    public async Task GetUserByIdAsync_ExistingUser_User()
    {
        // Arrange
        var expectedUser = UsersProvider.GetFirstUser();
        
        var repositoryMock = new Mock<IUserRepository>();
        repositoryMock
            .Setup(ur => ur.GetUserByIdAsync(It.Is<long>(l => l == expectedUser.UserId)))
            .Returns(Task.FromResult(expectedUser)!);

        var repository = repositoryMock.Object;
        var service = UserServiceProvider.GetUserService(repository);
        
        // Act
        var actualUser = await service.GetUserByIdAsync(expectedUser.UserId); 

        // Asserts
        actualUser.Should().BeEquivalentTo(expectedUser);
        repositoryMock.Verify(ur => ur.GetUserByIdAsync(expectedUser.UserId), Times.Once);
    }
    
    [Fact]
    public async Task GetUserByIdAsync_NonExistingUser_Null()
    {
        const int userId = 1;
        
        var repositoryMock = new Mock<IUserRepository>();
        repositoryMock
            .Setup(ur => ur.GetUserByIdAsync(It.Is<long>(l => l == userId)))
            .Returns(Task.FromResult<User>(null!)!);

        var repository = repositoryMock.Object;
        var service = UserServiceProvider.GetUserService(repository);
        
        // Act
        var act = async () => await service.GetUserByIdAsync(userId);

        // Asserts
        await act.Should().ThrowAsync<UserNotFoundException>();
        repositoryMock.Verify(ur => ur.GetUserByIdAsync(userId), Times.Once);
    }
    
    [Fact]
    public async Task RemoveUserByIdAsync_ExistingUser_User()
    {
        // Arrange
        var expectedUser = UsersProvider.GetFirstUser();
        
        var repositoryMock = new Mock<IUserRepository>();
        repositoryMock
            .Setup(ur => ur.RemoveUserByIdAsync(It.Is<long>(l => l == expectedUser.UserId)))
            .Returns(Task.FromResult(expectedUser)!);

        var repository = repositoryMock.Object;
        var service = UserServiceProvider.GetUserService(repository);
        
        // Act
        var act = async () => await service.RemoveUserById(expectedUser.UserId); 

        // Asserts
        await act.Should().NotThrowAsync();
        repositoryMock.Verify(ur => ur.RemoveUserByIdAsync(expectedUser.UserId), Times.Once);
    }
    
    [Fact]
    public async Task RemoveUserByIdAsync_NonExistingUser_Null()
    {
        const int userId = 1;
        
        var repositoryMock = new Mock<IUserRepository>();
        repositoryMock
            .Setup(ur => ur.RemoveUserByIdAsync(It.Is<long>(l => l == userId)))
            .Returns(Task.FromResult<User>(null!)!);

        var repository = repositoryMock.Object;
        var service = UserServiceProvider.GetUserService(repository);
        
        // Act
        var act = async () => await service.RemoveUserById(userId);

        // Asserts
        await act.Should().ThrowAsync<UserNotFoundException>();
        repositoryMock.Verify(ur => ur.RemoveUserByIdAsync(userId), Times.Once);
    }
}