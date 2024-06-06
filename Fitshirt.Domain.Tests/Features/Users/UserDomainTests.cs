using Fitshirt.Domain.Exceptions;
using Fitshirt.Domain.Features.Users;
using Fitshirt.Infrastructure.Models.Users;
using Fitshirt.Infrastructure.Models.Users.Entities;
using Fitshirt.Infrastructure.Repositories.Users;
using Moq;

namespace Fitshirt.Domain.Tests.Features.Users;

public class UserDomainTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IServiceRepository> _serviceRepositoryMock;
    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly UserDomain _userDomain;

    public UserDomainTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _serviceRepositoryMock = new Mock<IServiceRepository>();
        _roleRepositoryMock = new Mock<IRoleRepository>();

        _userDomain = new UserDomain(
            _userRepositoryMock.Object,
            _serviceRepositoryMock.Object,
            _roleRepositoryMock.Object
        );
    }

    [Fact]
    public async Task AddAsync_ValidData_ReturnsTrue()
    {
        // Arrange
        var user = new User
        {
            Email = "test@example.com",
            Cellphone = "987654321",
            Username = "testuser",
            BirthDate = DateOnly.Parse("2000-01-01")
        };
        
        var clientRole = new Role { Id = 1, Name = "Client" };
        var freeService = new Service { Id = 1, Name = "Free" };

        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(user.Email)).ReturnsAsync((User)null);
        _userRepositoryMock.Setup(repo => repo.GetUserByPhoneNumberAsync(user.Cellphone)).ReturnsAsync((User)null);
        _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(user.Username)).ReturnsAsync((User)null);
        _userRepositoryMock.Setup(repo => repo.AddAsync(user)).ReturnsAsync(true);
        _roleRepositoryMock.Setup(repo => repo.GetClientRoleAsync()).ReturnsAsync(clientRole);
        _serviceRepositoryMock.Setup(repo => repo.GetFreeServiceAsync()).ReturnsAsync(freeService);

        
        // Act
        var result =  await _userDomain.AddAsync(user);

        // Assert
        Assert.True(result);
        
        _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(user.Email), Times.Once);
        _userRepositoryMock.Verify(repo => repo.GetUserByPhoneNumberAsync(user.Cellphone), Times.Once);
        _userRepositoryMock.Verify(repo => repo.GetUserByUsernameAsync(user.Username), Times.Once);
        _userRepositoryMock.Verify(repo => repo.AddAsync(user), Times.Once);
        _roleRepositoryMock.Verify(repo => repo.GetClientRoleAsync(), Times.Once);
        _serviceRepositoryMock.Verify(repo => repo.GetFreeServiceAsync(), Times.Once);
    }

    [Fact]
    public async Task AddAsync_InvalidEmail_ThrowDuplicatedUserEmailException()
    {
        // Arrange
        var user = new User
        {
            Email = "test@example.com",
            Cellphone = "987654321",
            Username = "testuser",
            BirthDate = DateOnly.Parse("2000-01-01")
        };
        
        var existingUser = new User { Email = "test@example.com" };
        
        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(user.Email)).ReturnsAsync(existingUser);
        
        // Act
        var result = async () => await _userDomain.AddAsync(user);

        // Assert
        var exception = await Assert.ThrowsAsync<DuplicatedUserEmailException>(result);
        Assert.Equal(user.Email, exception.AttributeValue);
    }
    
    [Fact]
    public async Task AddAsync_InvalidPhoneNumber_ThrowDuplicatedUserCellphoneException()
    {
        // Arrange
        var user = new User
        {
            Email = "test@example.com",
            Cellphone = "987654321",
            Username = "testuser",
            BirthDate = DateOnly.Parse("2000-01-01")
        };
        
        var existingUser = new User { Cellphone = "987654321" };
        
        _userRepositoryMock
            .Setup(repo => repo.GetUserByEmailAsync(user.Email))
            .ReturnsAsync((User)null);
        _userRepositoryMock
            .Setup(repo => repo.GetUserByPhoneNumberAsync(user.Cellphone))
            .ReturnsAsync(existingUser);

        
        // Act
        var result = async () => await _userDomain.AddAsync(user);

        // Assert
        var exception = await Assert.ThrowsAsync<DuplicatedUserCellphoneException>(result);
        Assert.Equal(user.Cellphone, exception.AttributeValue);
    }
    
    [Fact]
    public async Task AddAsync_InvalidUsername_ThrowDuplicatedUserUsernameException()
    {
        // Arrange
        var user = new User
        {
            Email = "test@example.com",
            Cellphone = "987654321",
            Username = "testuser",
            BirthDate = DateOnly.Parse("2000-01-01")
        };
        
        var existingUser = new User { Username = "testuser" };
        
        _userRepositoryMock
            .Setup(repo => repo.GetUserByEmailAsync(user.Email))
            .ReturnsAsync((User)null);
        _userRepositoryMock
            .Setup(repo => repo.GetUserByPhoneNumberAsync(user.Cellphone))
            .ReturnsAsync((User)null);
        _userRepositoryMock
            .Setup(repo => repo.GetUserByUsernameAsync(user.Username))
            .ReturnsAsync(existingUser);

        
        // Act
        var result = async () => await _userDomain.AddAsync(user);

        // Assert
        var exception = await Assert.ThrowsAsync<DuplicatedUserUsernameException>(result);
        Assert.Equal(user.Username, exception.AttributeValue);
    }
    
    [Fact]
    public async Task AddAsync_InvalidBirthDate_ThrowValidationException()
    {
        // Arrange
        var user = new User
        {
            Email = "test@example.com",
            Cellphone = "987654321",
            Username = "testuser",
            BirthDate = DateOnly.Parse("2020-01-01")
        };
        
        _userRepositoryMock
            .Setup(repo => repo.GetUserByEmailAsync(user.Email))
            .ReturnsAsync((User)null);
        _userRepositoryMock
            .Setup(repo => repo.GetUserByPhoneNumberAsync(user.Cellphone))
            .ReturnsAsync((User)null);
        _userRepositoryMock
            .Setup(repo => repo.GetUserByUsernameAsync(user.Username))
            .ReturnsAsync((User)null);
        _userRepositoryMock.Setup(repo => repo.AddAsync(user)).ReturnsAsync(true);

        
        // Act
        var result = async () => await _userDomain.AddAsync(user);

        // Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(result);
        Assert.Equal("User must be, at least, 18 years old", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ValidData_ReturnsTrue()
    {
        // Arrange
        var userId = 1;
        var user = new User
        {
            Id = userId,
            Email = "test@example.com",
            Cellphone = "1234567890",
            Username = "testuser",
            BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-20)) // User is 20 years old
        };

        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(user.Email)).ReturnsAsync((User)null);
        _userRepositoryMock.Setup(repo => repo.GetUserByPhoneNumberAsync(user.Cellphone)).ReturnsAsync((User)null);
        _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(user.Username)).ReturnsAsync((User)null);
        _userRepositoryMock.Setup(repo => repo.UpdateAsync(userId, user)).ReturnsAsync(true);

        // Act
        var result = await _userDomain.UpdateAsync(userId, user);

        // Assert
        Assert.True(result);
        _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(user.Email), Times.Once);
        _userRepositoryMock.Verify(repo => repo.GetUserByPhoneNumberAsync(user.Cellphone), Times.Once);
        _userRepositoryMock.Verify(repo => repo.GetUserByUsernameAsync(user.Username), Times.Once);
        _userRepositoryMock.Verify(repo => repo.UpdateAsync(userId, user), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_InvalidEmail_ThrowDuplicatedUserEmailException()
    {
        // Arrange
        var userId = 1;
        var user = new User
        {
            Id = userId,
            Email = "test@example.com",
            Cellphone = "1234567890",
            Username = "testuser",
            BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-20)) // User is 20 years old
        };

        var existingUser = new User { Id = 2, Email = "test@example.com" }; // Different user with the same email

        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(user.Email)).ReturnsAsync(existingUser);

        // Act
        var result = async () => await _userDomain.UpdateAsync(userId, user);

        // Assert
        var exception = await Assert.ThrowsAsync<DuplicatedUserEmailException>(result);
        Assert.Equal(user.Email, exception.AttributeValue);
    }

    [Fact]
    public async Task UpdateAsync_InvalidCellphone_ThrowDuplicatedUserCellphoneException()
    {
        // Arrange
        var userId = 1;
        var user = new User
        {
            Id = userId,
            Email = "test@example.com",
            Cellphone = "1234567890",
            Username = "testuser",
            BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-20)) // User is 20 years old
        };

        var existingUser = new User { Id = 2, Cellphone = "1234567890" }; // Different user with the same cellphone

        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(user.Email)).ReturnsAsync((User)null);
        _userRepositoryMock.Setup(repo => repo.GetUserByPhoneNumberAsync(user.Cellphone)).ReturnsAsync(existingUser);

        // Act
        Func<Task> result = async () => await _userDomain.UpdateAsync(userId, user);

        // Assert
        var exception = await Assert.ThrowsAsync<DuplicatedUserCellphoneException>(result);
        Assert.Equal(user.Cellphone, exception.AttributeValue);
    }

    [Fact]
    public async Task UpdateAsync_InvalidUsername_ThrowDuplicatedUserUsernameException()
    {
        // Arrange
        var userId = 1;
        var user = new User
        {
            Id = userId,
            Email = "test@example.com",
            Cellphone = "1234567890",
            Username = "testuser",
            BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-20)) // User is 20 years old
        };

        var existingUser = new User { Id = 2, Username = "testuser" }; // Different user with the same username

        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(user.Email)).ReturnsAsync((User)null);
        _userRepositoryMock.Setup(repo => repo.GetUserByPhoneNumberAsync(user.Cellphone)).ReturnsAsync((User)null);
        _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(user.Username)).ReturnsAsync(existingUser);

        // Act
        Func<Task> result = async () => await _userDomain.UpdateAsync(userId, user);

        // Assert
        var exception = await Assert.ThrowsAsync<DuplicatedUserUsernameException>(result);
        Assert.Equal(user.Username, exception.AttributeValue);
    }

    [Fact]
    public async Task UpdateAsync_InvalidBirthdate_ThrowValidationException()
    {
        // Arrange
        var userId = 1;
        var user = new User
        {
            Id = userId,
            Email = "test@example.com",
            Cellphone = "1234567890",
            Username = "testuser",
            BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-17)) // User is 17 years old
        };

        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(user.Email)).ReturnsAsync((User)null);
        _userRepositoryMock.Setup(repo => repo.GetUserByPhoneNumberAsync(user.Cellphone)).ReturnsAsync((User)null);
        _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(user.Username)).ReturnsAsync((User)null);

        // Act
        Func<Task> result = async () => await _userDomain.UpdateAsync(userId, user);

        // Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(result);
        Assert.Equal("User must be, at least, 18 years old", exception.Message);
    }

    [Fact]
    public async Task VerifyLoginRequestAsync_ValidData_ReturnsTrue()
    {
        // Arrange
        var userLoginRequest = new User
        {
            Username = "testuser",
            Password = "correctpassword"
        };

        var userInDatabase = new User
        {
            Username = "testuser",
            Password = "correctpassword"
        };

        _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(userLoginRequest.Username)).ReturnsAsync(userInDatabase);

        // Act
        var result = await _userDomain.VerifyLoginRequestAsync(userLoginRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userLoginRequest.Username, result.Username);
    }

    [Fact]
    public async Task VerifyLoginRequestAsync_InvalidUsername_ThrowsNotFoundEntityAttributeException()
    {
        // Arrange
        var userLoginRequest = new User
        {
            Username = "incorrectuser",
            Password = "somepassword"
        };

        _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(userLoginRequest.Username)).ReturnsAsync((User)null);

        // Act
        Func<Task> result = async () => await _userDomain.VerifyLoginRequestAsync(userLoginRequest);

        // Assert
        var exception = await Assert.ThrowsAsync<NotFoundEntityAttributeException>(result);
        Assert.Equal(nameof(User), exception.EntityName);
        Assert.Equal(nameof(userLoginRequest.Username), exception.AttributeName);
        Assert.Equal(userLoginRequest.Username, exception.AttributeValue);
    }

    [Fact]
    public async Task VerifyLoginRequestAsync_InvalidPassword_ThrowsValidationException()
    {
        // Arrange
        var userLoginRequest = new User
        {
            Username = "testuser",
            Password = "incorrectpassword"
        };

        var userInDatabase = new User
        {
            Username = "testuser",
            Password = "correctpassword"
        };

        _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(userLoginRequest.Username)).ReturnsAsync(userInDatabase);

        // Act
        Func<Task> result = async () => await _userDomain.VerifyLoginRequestAsync(userLoginRequest);

        // Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(result);
        Assert.Equal("Incorrect password", exception.Message);
    }

}