using Fitshirt.Domain.Exceptions;
using Fitshirt.Infrastructure.Models.Common.Entities;
using Fitshirt.Infrastructure.Models.Posts;
using Fitshirt.Infrastructure.Models.Purchases;
using Fitshirt.Infrastructure.Models.Purchases.Entities;
using Fitshirt.Infrastructure.Models.Users;
using Fitshirt.Infrastructure.Repositories.Common.Entites;
using Fitshirt.Infrastructure.Repositories.Posts;
using Fitshirt.Infrastructure.Repositories.Purchases;
using Fitshirt.Infrastructure.Repositories.Users;

namespace Fitshirt.Domain.Features.Purchases;

public class PurchaseDomain : IPurchaseDomain
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;
    private readonly ISizeRepository _sizeRepository;

    public PurchaseDomain(IPurchaseRepository purchaseRepository, IUserRepository userRepository, IPostRepository postRepository, ISizeRepository sizeRepository)
    {
        _purchaseRepository = purchaseRepository;
        _userRepository = userRepository;
        _postRepository = postRepository;
        _sizeRepository = sizeRepository;
    }

    public async Task<bool> AddAsync(Purchase purchase)
    {
        purchase.PurchaseDate = DateTime.UtcNow;
        
        var user = await _userRepository.GetByIdAsync(purchase.UserId);

        if (user == null)
        {
            throw new NotFoundEntityIdException(nameof(User), purchase.UserId);
        }

        List<Item> items = new List<Item>();
        foreach (var itemRequest in purchase.Items)
        {
            var post = await _postRepository.GetByIdAsync(itemRequest.PostId);
            var size = await _sizeRepository.GetByIdAsync(itemRequest.SizeId);

            if (post == null)
            {
                throw new NotFoundEntityIdException(nameof(Post), itemRequest.PostId);
            }
            if (size == null)
            {
                throw new NotFoundEntityIdException(nameof(Size), itemRequest.SizeId);
            }

            if (itemRequest.Quantity > post.Stock)
            {
                throw new ValidationException("More quantity required than actual stock");
            }

            var item = new Item
            {
                Post = post,
                Size = size,
                Quantity = itemRequest.Quantity
            };
            
            items.Add(item);
        }

        purchase.Items = items;

        return await _purchaseRepository.AddAsync(purchase);
    }

    public Task<bool> UpdateAsync(int id, Purchase entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}