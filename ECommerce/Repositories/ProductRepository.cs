using E_commerce.Context;
using E_commerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly EdbContext _context;

    public ProductRepository(EdbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(p => p.ProductType) 
            .Include(p => p.Weights).ThenInclude(w => w.ProductType)
            .ToListAsync(cancellationToken);
    }

    public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.Weights).ThenInclude(w => w.ProductType)
            .FirstOrDefaultAsync(p => p.IdProduct == id, cancellationToken);
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    
    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken)
    { 
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    
    
    public async Task<string?> GetPhotoNameByProductIdAsync(int productId, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Where(p => p.IdProduct == productId)
            .Select(p => p.PhotoName)
            .FirstOrDefaultAsync(cancellationToken);

        return product ?? null;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    public async Task<List<Product>> GetRandomProductsAsync(int count, CancellationToken cancellationToken)
    {
        return await _context.Products
            .OrderBy(p => EF.Functions.Random()) // Use SQL NEWID() equivalent
            .Include(p => p.Weights).ThenInclude(w => w.ProductType)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
    
    // pagination basic
    
    public async Task<IEnumerable<Product>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(p => p.Weights).ThenInclude(w => w.ProductType)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
    
    // pagination for the type
    
    public async Task<IEnumerable<Product>> GetPaginatedByTypeAsync(int typeId, int page, int pageSize, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Where(p => p.IdProductType == typeId)
            .Include(p => p.Weights).ThenInclude(w => w.ProductType)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllByTypeAsync(int typeId, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Where(p => p.IdProductType == typeId)
            .Include(p => p.ProductType)
            .Include(p => p.Weights).ThenInclude(w => w.ProductType)
            .ToListAsync(cancellationToken);
    }
    
    
    
}