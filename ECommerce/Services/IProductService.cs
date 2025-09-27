using E_commerce.Entities;
using E_commerce.Models;

namespace ECommerce.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
    Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(CreateProductViewModel model, CancellationToken cancellationToken);
    Task UpdateAsync(int id, CreateProductViewModel model, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task<string?> GetPhotoNameByProductIdAsync(int productId, CancellationToken cancellationToken);
    
    //
    
    Task<List<Product>> GetRandomProductsAsync(int count, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetPaginatedProductsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    //
    Task<IEnumerable<Product>> GetPaginatedByTypeAsync(int typeId, int page, int pageSize, CancellationToken cancellationToken);
    
    Task<IEnumerable<Product>> GetAllByTypeAsync(int typeId, CancellationToken cancellationToken);
}