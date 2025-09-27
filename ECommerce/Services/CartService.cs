using E_commerce.Entities;
using ECommerce.Repositories;

namespace ECommerce.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<List<CartItem>> GetCartAsync(string userId)
    {
        return await _cartRepository.GetCartByUserIdAsync(userId);
    }

    // same product again => quantity + 1
    public async Task AddToCartAsync(string userId, int productId)
    {
        var cartItem = await _cartRepository.GetCartItemAsync(userId, productId);
        if (cartItem != null)
        {
            cartItem.Quantity++;
            await _cartRepository.UpdateAsync(cartItem);
            return;
        }

        await _cartRepository.AddAsync(new CartItem
        {
            UserId = userId,
            ProductId = productId,
            Quantity = 1
        });
    }

    public async Task RemoveFromCartAsync(string userId, int productId)
    {
        var cartItem = await _cartRepository.GetCartItemAsync(userId, productId);
        if (cartItem == null)
        {
            throw new InvalidOperationException("Product is not in the cart.");
        }

        await _cartRepository.RemoveAsync(cartItem);
    }
}
