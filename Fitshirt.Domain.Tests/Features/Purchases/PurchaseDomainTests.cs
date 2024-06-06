using Fitshirt.Domain.Exceptions;
using Fitshirt.Domain.Features.Purchases;
using Fitshirt.Infrastructure.Models.Common.Entities;
using Fitshirt.Infrastructure.Models.Posts;
using Fitshirt.Infrastructure.Models.Purchases;
using Fitshirt.Infrastructure.Models.Purchases.Entities;
using Fitshirt.Infrastructure.Models.Users;
using Fitshirt.Infrastructure.Repositories.Common.Entites;
using Fitshirt.Infrastructure.Repositories.Posts;
using Fitshirt.Infrastructure.Repositories.Purchases;
using Fitshirt.Infrastructure.Repositories.Users;
using Moq;

namespace Fitshirt.Domain.Tests.Features.Purchases;

public class PurchaseDomainTests
{
    private readonly Mock<IPurchaseRepository> _purchaseRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPostRepository> _postRepositoryMock;
    private readonly Mock<ISizeRepository> _sizeRepositoryMock;
    private readonly PurchaseDomain _purchaseDomain;

    public PurchaseDomainTests()
    {
        _purchaseRepositoryMock = new Mock<IPurchaseRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _postRepositoryMock = new Mock<IPostRepository>();
        _sizeRepositoryMock = new Mock<ISizeRepository>();

        _purchaseDomain = new PurchaseDomain(
            _purchaseRepositoryMock.Object,
            _userRepositoryMock.Object,
            _postRepositoryMock.Object,
            _sizeRepositoryMock.Object
        );
    }

    [Fact]
    public async Task AddPurchaseAsync_ValidPurchase_ReturnsTrue()
    {
        // Arrange
        var purchase = new Purchase
        {
            UserId = 1,
            Items = new List<Item>
            {
                new Item { PostId = 1, SizeId = 1, Quantity = 2 },
                new Item { PostId = 2, SizeId = 2, Quantity = 3 }
            }
        };

        var user = new User { Id = 1 };
        var post1 = new Post { Id = 1, Stock = 10 };
        var post2 = new Post { Id = 2, Stock = 10 };
        var size1 = new Size { Id = 1 };
        var size2 = new Size { Id = 2 };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(user);
        _postRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(post1);
        _postRepositoryMock.Setup(repo => repo.GetByIdAsync(2)).ReturnsAsync(post2);
        _sizeRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(size1);
        _sizeRepositoryMock.Setup(repo => repo.GetByIdAsync(2)).ReturnsAsync(size2);
        _purchaseRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Purchase>())).ReturnsAsync(true);

        // Act
        var result = await _purchaseDomain.AddAsync(purchase);

        // Assert
        Assert.True(result);
        
        _userRepositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        _postRepositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        _postRepositoryMock.Verify(repo => repo.GetByIdAsync(2), Times.Once);
        _sizeRepositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        _sizeRepositoryMock.Verify(repo => repo.GetByIdAsync(2), Times.Once);
        _purchaseRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Purchase>()), Times.Once);
    }
    
    [Fact]
    public async Task AddPurchaseAsync_InvalidUserId_ThrowsNotFoundEntityIdException()
    {
        // Arrange
        const int notExistingUserId = 9999;
        var purchase = new Purchase
        {
            UserId = notExistingUserId,
            Items = new List<Item>
            {
                new Item { PostId = 1, SizeId = 1, Quantity = 1},
            }
        };

        var post = new Post { Id = 1 };
        var size = new Size { Id = 1 };

        _userRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync((User)null);
        _postRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(post);
        _sizeRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(size);

        // Act
        var result = async () => await _purchaseDomain.AddAsync(purchase);

        // Assert
        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("User", exception.EntityName);
        Assert.Equal(notExistingUserId, exception.AttributeValue);
    }
    
    [Fact]
    public async Task AddPurchaseAsync_InvalidPostId_ThrowsNotFoundEntityIdException()
    {
        // Arrange
        const int notExistingPostId = 9999;
        var purchase = new Purchase
        {
            UserId = 1,
            Items = new List<Item>
            {
                new Item { PostId = notExistingPostId, SizeId = 1, Quantity = 1},
            }
        };
        
        var user = new User { Id = 1 };
        var size = new Size { Id = 1 };
        
        _userRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(user);
        _postRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync((Post)null);
        _sizeRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(size);

        // Act
        var result = async () => await _purchaseDomain.AddAsync(purchase);

        // Assert
        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Post", exception.EntityName);
        Assert.Equal(notExistingPostId, exception.AttributeValue);
    }
    
    [Fact]
    public async Task AddPurchaseAsync_InvalidSizeId_ThrowsNotFoundEntityIdException()
    {
        // Arrange
        const int notExistingSizeId = 9999;
        var purchase = new Purchase
        {
            UserId = 1,
            Items = new List<Item>
            {
                new Item { PostId = 1, SizeId = notExistingSizeId, Quantity = 1},
            }
        };
        
        var user = new User { Id = 1 };
        var post = new Post { Id = 1 };
        
        _userRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(user);
        _postRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(post);
        _sizeRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync((Size)null);

        // Act
        var result = async () => await _purchaseDomain.AddAsync(purchase);

        // Assert
        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Size", exception.EntityName);
        Assert.Equal(notExistingSizeId, exception.AttributeValue);
    }
    
    [Fact]
    public async Task AddPurchaseAsync_InvalidQuantity_ThrowsValidationException()
    {
        // Arrange
        const int quantity = 9999;
        var purchase = new Purchase
        {
            UserId = 1,
            Items = new List<Item>
            {
                new Item { PostId = 1, SizeId = 1, Quantity = quantity},
            }
        };
        
        var user = new User { Id = 1 };
        var post = new Post { Id = 1 , Stock = 1};
        var size = new Size { Id = 1 };
        
        _userRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(user);
        _postRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(post);
        _sizeRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(size);

        // Act
        var result = async () => await _purchaseDomain.AddAsync(purchase);

        // Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(result);
        Assert.Equal("More quantity required than actual stock", exception.Message);
    }
}