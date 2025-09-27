using E_commerce.Entities;

namespace ECommerce.Repositories;

public interface IVectorRepository
{
    Task<List<UserWeight>> GetUserWeightsAsync(string userId);
    Task AddUserWeightsAsync(IEnumerable<UserWeight> weights);
    Task<List<ProductWeight>> GetProductWeightsAsync(int productId);
    Task<List<Product>> GetProductsWithWeightsAsync();
    Task SaveChangesAsync();
}
