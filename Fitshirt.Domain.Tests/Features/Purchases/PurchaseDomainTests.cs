using Fitshirt.Domain.Features.Purchases;
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
    private readonly Mock<IItemRepository> _itemRepositoryMock;
    private readonly PurchaseDomain _purchaseDomain;

    public PurchaseDomainTests()
    {
        _purchaseRepositoryMock = new Mock<IPurchaseRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _itemRepositoryMock = new Mock<IItemRepository>();

        _purchaseDomain = new PurchaseDomain(
            _purchaseRepositoryMock.Object,
            _userRepositoryMock.Object,
            _itemRepositoryMock.Object
        );
    }

    [Fact]

    public async Task AddPurchaseAsync_ValidPurchase_ReturnsTrue()
    {
        var purchase = new Purchase
        {
            UserId = 1,
        };

        var items = new List<int> { 1, 2 };
        
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(purchase.UserId))
            .ReturnsAsync(new User { Id = purchase.UserId });
        _postRepositoryMock.Setup(repo => repo.(items))
            .ReturnsAsync(new List<Item>
            {
                
            })
    }
}