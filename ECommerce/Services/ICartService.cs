using E_commerce.Entities;

namespace ECommerce.Services;

public interface ICartService
{
    Task<List<CartItem>> GetCartAsync(string userId);
    Task AddToCartAsync(string userId, int productId);
    Task RemoveFromCartAsync(string userId, int productId);
}
