using E_commerce.Context;
using E_commerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories;

public class WishlistRepository : IWishlistRepository
{
    private readonly EdbContext _context;

    public WishlistRepository(EdbContext context)
    {
        _context = context;
    }

    public async Task<bool> WishlistItemExistsAsync(string userId, int productId)
    {
        return await _context.Wishlists.AnyAsync(w => w.UserId == userId && w.ProductId == productId);
    }

    public async Task AddToWishlistAsync(WishList wishlistItem)
    {
        _context.Wishlists.Add(wishlistItem);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Product>> GetWishlistByUserIdAsync(string userId)
    {
        return await _context.Products
            .Where(p => _context.Wishlists.Any(w => w.UserId == userId && w.ProductId == p.IdProduct))
            .Include(p => p.Weights).ThenInclude(w => w.ProductType)
            .ToListAsync();
    }
    
    public async Task RemoveFromWishlistAsync(string userId, int productId)
    {
        var wishlistItem = await _context.Wishlists
            .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);

        if (wishlistItem == null)
        {
            throw new InvalidOperationException("Product is not in the wishlist.");
        }

        _context.Wishlists.Remove(wishlistItem);
        await _context.SaveChangesAsync();
    }
}