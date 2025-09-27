using E_commerce.Entities;
using ECommerce.Repositories;

namespace ECommerce.Services;

public class WishlistService : IWishlistService
{
    private readonly IWishlistRepository _wishlistRepository;

    public WishlistService(IWishlistRepository wishlistRepository)
    {
        _wishlistRepository = wishlistRepository;
    }

    public async Task AddToWishlistAsync(string userId, int productId)
    {
        if (await _wishlistRepository.WishlistItemExistsAsync(userId, productId))
        {
            throw new InvalidOperationException("Product already in wishlist.");
        }

        var wishlistItem = new WishList
        {
            UserId = userId,
            ProductId = productId
        };

        await _wishlistRepository.AddToWishlistAsync(wishlistItem);
    }

    public async Task<List<Product>> GetWishlistAsync(string userId)
    {
        return await _wishlistRepository.GetWishlistByUserIdAsync(userId);
    }

    public async Task<bool> WishlistItemExistsAsync(string userId, int productId)
    {
        return await _wishlistRepository.WishlistItemExistsAsync(userId, productId);
    }
    
    public async Task RemoveFromWishlistAsync(string userId, int productId)
    {
        await _wishlistRepository.RemoveFromWishlistAsync(userId, productId);
    }
}