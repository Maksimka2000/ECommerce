using E_commerce.Entities;

namespace ECommerce.Repositories;

public interface IProductTypeRepository
{
    Task<IEnumerable<ProductType>> GetAllAsync(CancellationToken cancellationToken);
    IEnumerable<ProductType> GetAllAsync();
    Task<ProductType> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(ProductType productType, CancellationToken cancellationToken);
    Task UpdateAsync(ProductType productType, CancellationToken cancellationToken);
    Task DeleteAsync(ProductType productType, CancellationToken cancellationToken);
    

    
    
}