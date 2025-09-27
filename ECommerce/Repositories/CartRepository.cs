using E_commerce.Context;
using E_commerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories;

public class CartRepository : ICartRepository
{
    private readonly EdbContext _context;

    public CartRepository(EdbContext context)
    {
        _context = context;
    }

    public async Task<List<CartItem>> GetCartByUserIdAsync(string userId)
    {
        return await _context.CartItems
            .Where(c => c.UserId == userId)
            .Include(c => c.Product).ThenInclude(p => p.ProductType)
            .OrderBy(c => c.Id)
            .ToListAsync();
    }

    public async Task<CartItem?> GetCartItemAsync(string userId, int productId)
    {
        return await _context.CartItems
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
    }

    public async Task AddAsync(CartItem cartItem)
    {
        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CartItem cartItem)
    {
        _context.CartItems.Update(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(CartItem cartItem)
    {
        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task ClearAsync(string userId)
    {
        var items = await _context.CartItems.Where(c => c.UserId == userId).ToListAsync();
        _context.CartItems.RemoveRange(items);
        await _context.SaveChangesAsync();
    }
}
