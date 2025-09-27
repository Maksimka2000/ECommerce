using E_commerce.Context;
using E_commerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories;

public class VectorRepository : IVectorRepository
{
    private readonly EdbContext _context;

    public VectorRepository(EdbContext context)
    {
        _context = context;
    }

    public async Task<List<UserWeight>> GetUserWeightsAsync(string userId)
    {
        return await _context.UserWeights
            .Where(w => w.UserId == userId)
            .ToListAsync();
    }

    public async Task AddUserWeightsAsync(IEnumerable<UserWeight> weights)
    {
        _context.UserWeights.AddRange(weights);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ProductWeight>> GetProductWeightsAsync(int productId)
    {
        return await _context.ProductWeights
            .Where(w => w.IdProduct == productId)
            .ToListAsync();
    }

    public async Task<List<Product>> GetProductsWithWeightsAsync()
    {
        return await _context.Products
            .Include(p => p.Weights).ThenInclude(w => w.ProductType)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
