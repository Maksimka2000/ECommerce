using E_commerce.Entities;
using E_commerce.Models;

namespace ECommerce.Services;

public interface IProductTypeService
{
    Task<IEnumerable<ProductType>> GetAllAsync(CancellationToken cancellationToken);
    IEnumerable<ProductType> GetAllAsync();
    Task<ProductType> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(CreateProductTypeViewModel model, CancellationToken cancellationToken);
    Task UpdateAsync(int id, CreateProductTypeViewModel model, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}