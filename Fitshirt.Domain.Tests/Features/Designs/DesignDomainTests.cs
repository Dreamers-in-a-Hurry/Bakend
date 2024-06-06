using Fitshirt.Domain.Exceptions;
using Fitshirt.Domain.Features.Designs;
using Fitshirt.Infrastructure.Models.Common.Entities;
using Fitshirt.Infrastructure.Models.Designs;
using Fitshirt.Infrastructure.Models.Designs.Entities;
using Fitshirt.Infrastructure.Models.Users;
using Fitshirt.Infrastructure.Repositories.Common.Entites;
using Fitshirt.Infrastructure.Repositories.Designs;
using Fitshirt.Infrastructure.Repositories.Users;
using Moq;

namespace Fitshirt.Domain.Tests.Features.Designs;

public class DesignDomainTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IColorRepository> _colorRepositoryMock;
    private readonly Mock<IShieldRepository> _shieldRepositoryMock;
    private readonly Mock<IDesignRepository> _designRepositoryMock;
    private readonly DesignDomain _designDomain;

    public DesignDomainTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _shieldRepositoryMock = new Mock<IShieldRepository>();
        _colorRepositoryMock = new Mock<IColorRepository>();
        _designRepositoryMock = new Mock<IDesignRepository>();

        _designDomain = new DesignDomain(
            _designRepositoryMock.Object,
            _colorRepositoryMock.Object,
            _shieldRepositoryMock.Object,
            _userRepositoryMock.Object
        );
    }

    [Fact]

    public async Task AddDesignAsync_ValidDesign_ReturnsTrue()
    {
        var design = new Design
        {
            UserId = 1,
            PrimaryColorId = 1,
            SecondaryColorId = 1,
            TertiaryColorId = 1,
            ShieldId = 1
        };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(design.UserId))
            .ReturnsAsync(new User { Id = design.UserId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.PrimaryColorId))
            .ReturnsAsync(new Color { Id = design.PrimaryColorId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.SecondaryColorId))
            .ReturnsAsync(new Color { Id = design.SecondaryColorId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.TertiaryColorId))
            .ReturnsAsync(new Color { Id = design.TertiaryColorId });
        _shieldRepositoryMock.Setup(repo => repo.GetByIdAsync(design.ShieldId))
            .ReturnsAsync(new Shield { Id = design.ShieldId });

        _designRepositoryMock.Setup(repository => repository.AddAsync(design)).ReturnsAsync(true);

        var result = await _designDomain.AddDesignAsync(design);

        Assert.True(result);

        _userRepositoryMock.Verify(repo => repo.GetByIdAsync(design.UserId), Times.Once);
        _colorRepositoryMock.Verify(repo => repo.GetByIdAsync(design.PrimaryColorId), Times.Exactly(3));
        _colorRepositoryMock.Verify(repo => repo.GetByIdAsync(design.SecondaryColorId), Times.Exactly(3));
        _colorRepositoryMock.Verify(repo => repo.GetByIdAsync(design.TertiaryColorId), Times.Exactly(3));
        _shieldRepositoryMock.Verify(repo => repo.GetByIdAsync(design.ShieldId), Times.Once);
        _designRepositoryMock.Verify(repo => repo.AddAsync(design), Times.Once);
    }

    [Fact]
    public async Task AddDesignAsync_InvalidUserId_ThrowsNotFoundEntityIdException()
    {
        var notExistingUserId = 9999;
        var design = new Design
        {
            UserId = notExistingUserId,
            PrimaryColorId = 1,
            SecondaryColorId = 1,
            TertiaryColorId = 1,
            ShieldId = 1
        };
        
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(design.UserId))
            .ReturnsAsync((User)null);
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.PrimaryColorId))
            .ReturnsAsync(new Color { Id = design.PrimaryColorId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.SecondaryColorId))
            .ReturnsAsync(new Color { Id = design.SecondaryColorId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.TertiaryColorId))
            .ReturnsAsync(new Color { Id = design.TertiaryColorId });
        _shieldRepositoryMock.Setup(repo => repo.GetByIdAsync(design.ShieldId))
            .ReturnsAsync(new Shield { Id = design.ShieldId });

        var result = async () => await _designDomain.AddDesignAsync(design);

        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("User", exception.EntityName);
        Assert.Equal(notExistingUserId, exception.AttributeValue);
    }

    [Fact]
    public async Task AddDesignAsync_InvalidPrimaryColorId_ThrowsNotFoundEntityIdException()
    {
        var notExistingPrimaryColorId = 9999;
        var design = new Design
        {
            PrimaryColorId = notExistingPrimaryColorId
        };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(design.UserId))
            .ReturnsAsync(new User { Id = design.UserId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.PrimaryColorId))
            .ReturnsAsync((Color)null);
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.SecondaryColorId))
            .ReturnsAsync(new Color { Id = design.SecondaryColorId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.TertiaryColorId))
            .ReturnsAsync(new Color { Id = design.TertiaryColorId });
        _shieldRepositoryMock.Setup(repo => repo.GetByIdAsync(design.ShieldId))
            .ReturnsAsync(new Shield { Id = design.ShieldId });

        var result = async () => await _designDomain.AddDesignAsync(design);

        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Color", exception.EntityName);
        Assert.Equal(notExistingPrimaryColorId, exception.AttributeValue);
    }

    [Fact]
    public async Task AddDesignAsync_InvalidSecondaryColorId_ThrowsNotFoundEntityIdException()
    {
        var notExistingSecondaryColorId = 9999;
        var design = new Design
        {
            SecondaryColorId = notExistingSecondaryColorId
        };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(design.UserId))
            .ReturnsAsync(new User { Id = design.UserId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.PrimaryColorId))
            .ReturnsAsync(new Color { Id = design.PrimaryColorId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.SecondaryColorId))
            .ReturnsAsync((Color)null);
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.TertiaryColorId))
            .ReturnsAsync(new Color { Id = design.TertiaryColorId });
        _shieldRepositoryMock.Setup(repo => repo.GetByIdAsync(design.ShieldId))
            .ReturnsAsync(new Shield { Id = design.ShieldId });

        var result = async () => await _designDomain.AddDesignAsync(design);

        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Color", exception.EntityName);
        Assert.Equal(notExistingSecondaryColorId, exception.AttributeValue);
    }

    [Fact]
    public async Task AddDesignAsync_InvalidTertiaryColorId_ThrowsNotFoundEntityIdException()
    {
        var notExistingTertiaryColorId = 9999;
        var design = new Design
        {
            TertiaryColorId = notExistingTertiaryColorId
        };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(design.UserId))
            .ReturnsAsync(new User { Id = design.UserId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.PrimaryColorId))
            .ReturnsAsync(new Color { Id = design.PrimaryColorId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.SecondaryColorId))
            .ReturnsAsync(new Color {Id = design.SecondaryColorId});
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.TertiaryColorId))
            .ReturnsAsync((Color)null);
        _shieldRepositoryMock.Setup(repo => repo.GetByIdAsync(design.ShieldId))
            .ReturnsAsync(new Shield { Id = design.ShieldId });

        var result = async () => await _designDomain.AddDesignAsync(design);

        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Color", exception.EntityName);
        Assert.Equal(notExistingTertiaryColorId, exception.AttributeValue);
    }

    [Fact]

    public async Task AddDesignAsync_InvalidShieldId_ThrowsNotFoundEntityIdException()
    {
        var notExistingShieldId = 9999;
        var design = new Design
        {
            ShieldId = notExistingShieldId
        };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(design.UserId))
            .ReturnsAsync(new User { Id = design.UserId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.PrimaryColorId))
            .ReturnsAsync(new Color { Id = design.PrimaryColorId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.SecondaryColorId))
            .ReturnsAsync(new Color {Id = design.SecondaryColorId});
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.TertiaryColorId))
            .ReturnsAsync(new Color { Id = design.TertiaryColorId });
        _shieldRepositoryMock.Setup(repo => repo.GetByIdAsync(design.ShieldId))
            .ReturnsAsync((Shield)null);

        var result = async () => await _designDomain.AddDesignAsync(design);

        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Shield", exception.EntityName);
        Assert.Equal(notExistingShieldId, exception.AttributeValue);
    }

    [Fact]

    public async Task DeleteAsync_ValidData_ReturnsTrue()
    {
        var design = new Design()
        {
            Id = 1
        };

        _designRepositoryMock.Setup(repo => repo.GetByIdAsync(design.Id))
            .ReturnsAsync(design);
        _designRepositoryMock.Setup(repo => repo.DeleteAsync(design.Id))
            .ReturnsAsync(true);

        var result = await _designDomain.DeleteAsync(design.Id);

        Assert.True(result);
    }

    [Fact]

    public async Task DeleteAsync_InvalidDesignId_ThrowsNotFoundEntityIdException()
    {
        var idNotFound = 999;
        var design = new Design()
        {
            Id = idNotFound
        };

        _designRepositoryMock.Setup(repo => repo.GetByIdAsync(design.Id))
            .ReturnsAsync((Design)null);

        var result = async () => await _designDomain.DeleteAsync(design.Id);

        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Design", exception.EntityName);
        Assert.Equal(idNotFound, exception.AttributeValue);
    }

    [Fact]

    public async Task UpdateDesignAsync_ValidData_ReturnsTrue()
    {
        var design = new Design
        {
            Id = 1
        };

        _designRepositoryMock.Setup(repo => repo.GetByIdAsync(design.Id))
            .ReturnsAsync(design);
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(design.UserId))
            .ReturnsAsync(new User { Id = design.UserId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.PrimaryColorId))
            .ReturnsAsync(new Color { Id = design.PrimaryColorId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.SecondaryColorId))
            .ReturnsAsync(new Color { Id = design.SecondaryColorId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.TertiaryColorId))
            .ReturnsAsync(new Color { Id = design.TertiaryColorId });
        _shieldRepositoryMock.Setup(repo => repo.GetByIdAsync(design.ShieldId))
            .ReturnsAsync(new Shield { Id = design.ShieldId });

        _designRepositoryMock.Setup(repo => repo.UpdateAsync(design.Id, design))
            .ReturnsAsync(true);

        var result = await _designDomain.UpdateDesignAsync(design.Id, design);

        Assert.True(result);

        _userRepositoryMock.Verify(repo => repo.GetByIdAsync(design.UserId), Times.Once);
        _colorRepositoryMock.Verify(repo => repo.GetByIdAsync(design.PrimaryColorId), Times.Exactly(3));
        _colorRepositoryMock.Verify(repo => repo.GetByIdAsync(design.SecondaryColorId), Times.Exactly(3));
        _colorRepositoryMock.Verify(repo => repo.GetByIdAsync(design.TertiaryColorId), Times.Exactly(3));
        _shieldRepositoryMock.Verify(repo => repo.GetByIdAsync(design.ShieldId), Times.Once);
        _designRepositoryMock.Verify(repo => repo.UpdateAsync(design.Id, design), Times.Once);
    }

    [Fact]
    public async Task UpdateDesignAsync_InvalidDesignId_ThrowsNotFoundEntityIdException()
    {
        var idNotFound = 999;
        var design = new Design()
        {
            Id = idNotFound
        };

        _designRepositoryMock.Setup(repo => repo.GetByIdAsync(design.Id))
            .ReturnsAsync((Design)null);

        var result = async () => await _designDomain.UpdateDesignAsync(idNotFound, design);

        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Design",exception.EntityName);
        Assert.Equal(idNotFound,exception.AttributeValue);
    }
        
        [Fact]
    public async Task UpdateDesignAsync_InvalidUserId_ThrowsNotFoundEntityIdException()
    {
        var idNotFound = 999;
        var design = new Design()
        {
            Id = 1,
            UserId = idNotFound,
        };
        
        _designRepositoryMock.Setup(repo => repo.GetByIdAsync(design.Id))
            .ReturnsAsync(new Design { Id = 1 });
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(design.UserId))
            .ReturnsAsync((User)null);
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.PrimaryColorId))
            .ReturnsAsync(new Color { Id = design.PrimaryColorId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.SecondaryColorId))
            .ReturnsAsync(new Color { Id = design.SecondaryColorId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.TertiaryColorId))
            .ReturnsAsync(new Color { Id = design.TertiaryColorId });
        _shieldRepositoryMock.Setup(repo => repo.GetByIdAsync(design.ShieldId))
            .ReturnsAsync(new Shield { Id = design.ShieldId });

        var result = async () => await _designDomain.UpdateDesignAsync(1,design);

        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("User", exception.EntityName);
        Assert.Equal(idNotFound, exception.AttributeValue);
    }

    [Fact]
    public async Task UpdateDesignAsync_InvalidPrimaryColorId_ThrowsNotFoundEntityIdException()
    {
        var idNotFound = 999;
        var design = new Design()
        {
            Id = 1,
            PrimaryColorId = 999
        };
        
        _designRepositoryMock.Setup(repo => repo.GetByIdAsync(design.Id))
            .ReturnsAsync(new Design { Id = 1 });
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(design.UserId))
            .ReturnsAsync(new User { Id = 1 });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.PrimaryColorId))
            .ReturnsAsync((Color)null);
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.SecondaryColorId))
            .ReturnsAsync(new Color { Id = 1 });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.TertiaryColorId))
            .ReturnsAsync(new Color { Id = 1 });
        _shieldRepositoryMock.Setup(repo => repo.GetByIdAsync(design.ShieldId))
            .ReturnsAsync(new Shield { Id = 1 });


        var result = async () => await _designDomain.UpdateDesignAsync(1,design);

        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Color", exception.EntityName);
        Assert.Equal(idNotFound, exception.AttributeValue);
    }

    [Fact]
    public async Task UpdateDesignAsync_InvalidSecondaryColorId_ThrowsNotFoundEntityIdException()
    {
        var idNotFound = 999;
        var design = new Design()
        {
            SecondaryColorId = 999
        };

        _designRepositoryMock.Setup(repo => repo.GetByIdAsync(design.Id))
            .ReturnsAsync(new Design { Id = 1 });
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(design.UserId))
            .ReturnsAsync(new User { Id = design.UserId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.PrimaryColorId))
            .ReturnsAsync(new Color { Id = design.PrimaryColorId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.SecondaryColorId))
            .ReturnsAsync((Color)null);
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.TertiaryColorId))
            .ReturnsAsync(new Color { Id = design.TertiaryColorId });
        _shieldRepositoryMock.Setup(repo => repo.GetByIdAsync(design.ShieldId))
            .ReturnsAsync(new Shield { Id = design.ShieldId });

        var result = async () => await _designDomain.AddDesignAsync(design);

        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Color", exception.EntityName);
        Assert.Equal(idNotFound, exception.AttributeValue);
    }

    [Fact]
    public async Task UpdateDesignAsync_InvalidTertiaryColorId_ThrowsNotFoundEntityIdException()
    {
        var idNotFound = 999;
        var design = new Design
        {
            Id = 1,
            TertiaryColorId = 999
        };

        _designRepositoryMock.Setup(repo => repo.GetByIdAsync(design.Id))
            .ReturnsAsync(new Design { Id = 1 });
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(design.UserId))
            .ReturnsAsync(new User { Id = design.UserId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.PrimaryColorId))
            .ReturnsAsync(new Color { Id = design.PrimaryColorId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.SecondaryColorId))
            .ReturnsAsync(new Color {Id = design.SecondaryColorId});
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.TertiaryColorId))
            .ReturnsAsync((Color)null);
        _shieldRepositoryMock.Setup(repo => repo.GetByIdAsync(design.ShieldId))
            .ReturnsAsync(new Shield { Id = design.ShieldId });

        var result = async () => await _designDomain.UpdateDesignAsync(1,design);

        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Color", exception.EntityName);
        Assert.Equal(idNotFound, exception.AttributeValue);
    }

    [Fact]
    public async Task UpdateDesignAsync_InvalidShieldId_ThrowsNotFoundEntityIdException()
    {
        var idNotFound = 999;
        var design = new Design()
        {
            Id = 1,
            ShieldId = idNotFound
        };

        _designRepositoryMock.Setup(repo => repo.GetByIdAsync(design.Id))
            .ReturnsAsync(new Design { Id = 1 });
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(design.UserId))
            .ReturnsAsync(new User { Id = 1 });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.PrimaryColorId))
            .ReturnsAsync(new Color { Id = 1 });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.SecondaryColorId))
            .ReturnsAsync(new Color {Id = 1 });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(design.TertiaryColorId))
            .ReturnsAsync(new Color { Id = 1 });
        _shieldRepositoryMock.Setup(repo => repo.GetByIdAsync(design.ShieldId))
            .ReturnsAsync((Shield)null);

        var result = async () => await _designDomain.UpdateDesignAsync(1,design);

        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Shield", exception.EntityName);
        Assert.Equal(idNotFound, exception.AttributeValue);
    }
}
    