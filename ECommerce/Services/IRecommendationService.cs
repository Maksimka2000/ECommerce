using E_commerce.Entities;
using E_commerce.Models;

namespace ECommerce.Services;

public interface IRecommendationService
{
    // user vector (created with random 0-2 values when the user has none yet)
    Task CreateRandomUserVectorAsync(string userId);
    Task<List<VectorValue>> GetUserVectorAsync(string userId);
    Task<List<VectorValue>> GetProductVectorAsync(int productId);

    // rules that change the user vector
    Task ApplyCategoryVisitAsync(string userId, int typeId, bool categoryChanged);
    Task ApplyProductActionAsync(string userId, int productId, double multiplier);
    Task ApplyProductsActionAsync(string userId, IEnumerable<int> productIds, double multiplier);

    // KNN with Euclidean distance
    Task<List<ProductDistance>> RankProductsAsync(string userId);
    Task<List<Product>> RecommendAsync(string userId, int k);
}
