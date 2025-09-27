using E_commerce.Context;
using E_commerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories;

public class ProductTypeRepository(EdbContext context) : IProductTypeRepository
{
    private readonly EdbContext _context = context;

    public  IEnumerable<ProductType> GetAllAsync()
    {
        return _context.ProductTypes.ToList();
    }
    public async Task<IEnumerable<ProductType>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.ProductTypes.ToListAsync(cancellationToken);
    }

    public async Task<ProductType> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.ProductTypes.FirstOrDefaultAsync(pt => pt.IdProductType == id, cancellationToken);
    }

    public async Task AddAsync(ProductType productType, CancellationToken cancellationToken)
    {
        await _context.ProductTypes.AddAsync(productType, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProductType productType, CancellationToken cancellationToken)
    {
        _context.ProductTypes.Update(productType);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ProductType productType, CancellationToken cancellationToken)
    {
            _context.ProductTypes.Remove(productType);
            await _context.SaveChangesAsync(cancellationToken);
        
    }
}