using E_commerce.Entities;

namespace ECommerce.Services;

public interface IWishlistService
{
    Task AddToWishlistAsync(string userId, int productId);
    Task<List<Product>> GetWishlistAsync(string userId);
    Task<bool> WishlistItemExistsAsync(string userId, int productId);
    Task RemoveFromWishlistAsync(string userId, int productId);
}