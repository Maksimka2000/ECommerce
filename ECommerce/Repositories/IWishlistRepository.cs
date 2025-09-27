using E_commerce.Entities;

namespace ECommerce.Repositories;

public interface IWishlistRepository
{
    Task<bool> WishlistItemExistsAsync(string userId, int productId);
    Task AddToWishlistAsync(WishList wishlistItem);
    Task<List<Product>> GetWishlistByUserIdAsync(string userId);
    Task RemoveFromWishlistAsync(string userId, int productId);
}