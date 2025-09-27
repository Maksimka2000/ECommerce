using E_commerce.Entities;
using E_commerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services;

// Multipliers of the vector rules. Positive = add interest, negative = remove interest.
public static class VectorAction
{
    public const double ViewProduct = 1;
    public const double AddToWishlist = 1.5;
    public const double RemoveFromWishlist = -1.25;
    public const double AddToCart = 1.75;
    public const double RemoveFromCart = -1.5;
    public const double FinalizeOrder = 2;
}

public class RecommendationService : IRecommendationService
{
    public const double MaxWeight = 10;
    public const double StartMaxWeight = 2;     // new user: random 0..2 for every category
    public const double CategoryVisitBonus = 0.5;
    public const double OtherCategoryPenalty = 0.1;

    private readonly IVectorRepository _vectorRepository;
    private readonly IProductTypeRepository _productTypeRepository;

    public RecommendationService(IVectorRepository vectorRepository, IProductTypeRepository productTypeRepository)
    {
        _vectorRepository = vectorRepository;
        _productTypeRepository = productTypeRepository;
    }


    // user vector ========================================================================================

    public async Task CreateRandomUserVectorAsync(string userId)
    {
        await LoadUserVectorAsync(userId);
    }

    public async Task<List<VectorValue>> GetUserVectorAsync(string userId)
    {
        var types = await GetTypesAsync();
        var vector = await LoadUserVectorAsync(userId);
        return types.Select(t => new VectorValue
        {
            IdProductType = t.IdProductType,
            TypeName = t.TypeName,
            Weight = vector[t.IdProductType].Weight,
            LastChange = vector[t.IdProductType].LastChange
        }).ToList();
    }

    public async Task<List<VectorValue>> GetProductVectorAsync(int productId)
    {
        var types = await GetTypesAsync();
        var weights = await _vectorRepository.GetProductWeightsAsync(productId);
        return ToVector(types, weights);
    }

    // Loads the user vector as {categoryId -> weight}.
    // Brand new user gets random 0..2 for every category; a category added later starts at 0.
    private async Task<Dictionary<int, UserWeight>> LoadUserVectorAsync(string userId)
    {
        var types = await GetTypesAsync();
        var weights = await _vectorRepository.GetUserWeightsAsync(userId);
        var isNewUser = weights.Count == 0;

        var missing = types
            .Where(t => weights.All(w => w.IdProductType != t.IdProductType))
            .Select(t => new UserWeight
            {
                UserId = userId,
                IdProductType = t.IdProductType,
                Weight = isNewUser ? Math.Round(Random.Shared.NextDouble() * StartMaxWeight, 2) : 0
            })
            .ToList();

        if (missing.Count > 0)
        {
            await _vectorRepository.AddUserWeightsAsync(missing);
            weights.AddRange(missing);
        }

        return weights.ToDictionary(w => w.IdProductType);
    }


    // rules ========================================================================================

    // Category page: +0.5 for the entered category.
    // If the user came from a different category: -0.1 for all other categories.
    public async Task ApplyCategoryVisitAsync(string userId, int typeId, bool categoryChanged)
    {
        var vector = await LoadUserVectorAsync(userId);
        var before = vector.ToDictionary(v => v.Key, v => v.Value.Weight);

        foreach (var (categoryId, userWeight) in vector)
        {
            if (categoryId == typeId)
                Change(userWeight, CategoryVisitBonus);
            else if (categoryChanged)
                Change(userWeight, -OtherCategoryPenalty);
        }

        RememberLastChange(vector, before);
        await _vectorRepository.SaveChangesAsync();
    }

    public Task ApplyProductActionAsync(string userId, int productId, double multiplier)
    {
        return ApplyProductsActionAsync(userId, new[] { productId }, multiplier);
    }

    // Product action (view, wishlist, cart, order) with product vector [swimming:8, clothing:4, other:1]:
    //   categories of the product: +-(weight / 10) * multiplier   -> swimming +0.8 * m
    //   all other categories:      -0.1 * |multiplier|
    // An order applies the rule for every product in it, but counts as one action for "last change".
    public async Task ApplyProductsActionAsync(string userId, IEnumerable<int> productIds, double multiplier)
    {
        var vector = await LoadUserVectorAsync(userId);
        var before = vector.ToDictionary(v => v.Key, v => v.Value.Weight);

        var strength = Math.Abs(multiplier);
        var sign = multiplier >= 0 ? 1 : -1;

        foreach (var productId in productIds)
        {
            var productWeights = (await _vectorRepository.GetProductWeightsAsync(productId))
                .ToDictionary(w => w.IdProductType, w => w.Weight);

            foreach (var (categoryId, userWeight) in vector)
            {
                var productWeight = productWeights.GetValueOrDefault(categoryId);

                if (productWeight > 0)
                    Change(userWeight, sign * productWeight / MaxWeight * strength);
                else
                    Change(userWeight, -OtherCategoryPenalty * strength);
            }
        }

        RememberLastChange(vector, before);
        await _vectorRepository.SaveChangesAsync();
    }

    // keeps every weight between 0 and 10
    // (rounded to 4 decimals only to remove floating point noise, e.g. 0.1 * 1.25 = 0.125 stays exact)
    private static void Change(UserWeight userWeight, double delta)
    {
        userWeight.Weight = Math.Round(Math.Clamp(userWeight.Weight + delta, 0, MaxWeight), 4);
    }

    // how much the last action really changed every weight (after the 0..10 limit), 0 = not changed
    private static void RememberLastChange(Dictionary<int, UserWeight> vector, Dictionary<int, double> before)
    {
        foreach (var (categoryId, userWeight) in vector)
        {
            userWeight.LastChange = Math.Round(userWeight.Weight - before[categoryId], 4);
        }
    }


    // KNN ========================================================================================

    // Euclidean distance from the user vector to every product, sorted from nearest to farthest:
    // d = sqrt( (u1 - p1)^2 + (u2 - p2)^2 + ... + (un - pn)^2 )
    public async Task<List<ProductDistance>> RankProductsAsync(string userId)
    {
        var types = await GetTypesAsync();
        var userVector = await LoadUserVectorAsync(userId);
        var products = await _vectorRepository.GetProductsWithWeightsAsync();

        var result = new List<ProductDistance>();
        foreach (var product in products)
        {
            var productVector = ToVector(types, product.Weights);

            double sumOfSquares = 0;
            foreach (var value in productVector)
            {
                var difference = userVector[value.IdProductType].Weight - value.Weight;
                sumOfSquares += difference * difference;
            }

            result.Add(new ProductDistance
            {
                Product = product,
                ProductVector = productVector,
                SumOfSquares = sumOfSquares,
                Distance = Math.Sqrt(sumOfSquares)
            });
        }

        return result
            .OrderBy(r => r.Distance)
            .ThenBy(r => r.Product.IdProduct)
            .ToList();
    }

    // K nearest neighbours = the K products with the smallest distance
    public async Task<List<Product>> RecommendAsync(string userId, int k)
    {
        var ranked = await RankProductsAsync(userId);
        return ranked.Take(k).Select(r => r.Product).ToList();
    }


    // helpers ========================================================================================

    private async Task<List<ProductType>> GetTypesAsync()
    {
        var types = await _productTypeRepository.GetAllAsync(CancellationToken.None);
        return types.OrderBy(t => t.IdProductType).ToList();
    }

    // full vector in category order; a category without a row counts as 0
    private static List<VectorValue> ToVector(List<ProductType> types, IEnumerable<ProductWeight>? weights)
    {
        var byType = (weights ?? Enumerable.Empty<ProductWeight>())
            .ToDictionary(w => w.IdProductType, w => w.Weight);

        return types.Select(t => new VectorValue
        {
            IdProductType = t.IdProductType,
            TypeName = t.TypeName,
            Weight = byType.GetValueOrDefault(t.IdProductType)
        }).ToList();
    }
}
