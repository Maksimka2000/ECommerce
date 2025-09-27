using E_commerce.Entities;

namespace ECommerce.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
    Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(Product product, CancellationToken cancellationToken);
    Task UpdateAsync(Product product, CancellationToken cancellationToken);
    Task DeleteAsync(Product product, CancellationToken cancellationToken);
    Task<string?> GetPhotoNameByProductIdAsync(int productId, CancellationToken cancellationToken);
    
    //
    
    
    Task<List<Product>> GetRandomProductsAsync(int count, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    
    //
    Task<IEnumerable<Product>> GetPaginatedByTypeAsync(int typeId, int page, int pageSize,
        CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetAllByTypeAsync(int typeId, CancellationToken cancellationToken);
}