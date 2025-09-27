using E_commerce.Entities;

namespace ECommerce.Repositories;

public interface ICartRepository
{
    Task<List<CartItem>> GetCartByUserIdAsync(string userId);
    Task<CartItem?> GetCartItemAsync(string userId, int productId);
    Task AddAsync(CartItem cartItem);
    Task UpdateAsync(CartItem cartItem);
    Task RemoveAsync(CartItem cartItem);
    Task ClearAsync(string userId);
}
