using DoctorAppCodeFirst.Exceptions;
using E_commerce.Entities;
using E_commerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _productRepository.GetAllAsync(cancellationToken);
    }
    public async Task AddAsync(CreateProductViewModel model, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Availability = model.Availability,
            IdProductType = model.IdProductType,
            PhotoName = model.PhotoName,
            // product vector: its own category gets the max weight, other categories count as 0
            Weights = new List<ProductWeight>
            {
                new ProductWeight { IdProductType = model.IdProductType, Weight = RecommendationService.MaxWeight }
            }
        };

        await _productRepository.AddAsync(product, cancellationToken);
    }
    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        EnsureProductExist(product, id);
        await _productRepository.DeleteAsync(product, cancellationToken);
    }

    public async Task<string?> GetPhotoNameByProductIdAsync(int productId, CancellationToken cancellationToken)
    {
        return await _productRepository.GetPhotoNameByProductIdAsync(productId, cancellationToken);
    }

    public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(id, cancellationToken);
    }
    
    
    public async Task UpdateAsync(int id, CreateProductViewModel model, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            IdProduct = id,
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Availability = model.Availability,
            IdProductType = model.IdProductType,
            PhotoName = model.PhotoName 
        };
        
        await _productRepository.UpdateAsync(product, cancellationToken);
    }
    
    
    
    
    
    // Validation
    private void EnsureProductExist(Product? product, int productId)
    {
        if (product == null) throw new DomainException($"There is no client with id {productId} ");  
    }
    
    
    
    
    
    
    //==
    
    public async Task<List<Product>> GetRandomProductsAsync(int count, CancellationToken cancellationToken)
    {
        return await _productRepository.GetRandomProductsAsync(count, cancellationToken);
    }
    public async Task<IEnumerable<Product>> GetPaginatedProductsAsync(int pageNumber, int pageSize,  CancellationToken cancellationToken)
    {
        return await _productRepository.GetPaginatedAsync(pageNumber, pageSize, cancellationToken);
    }
    
    public async Task<IEnumerable<Product>> GetPaginatedByTypeAsync(int typeId, int page, int pageSize, CancellationToken cancellationToken)
    {
        return await _productRepository.GetPaginatedByTypeAsync(typeId, page, pageSize, cancellationToken);
    }
    
    public async Task<IEnumerable<Product>> GetAllByTypeAsync(int typeId, CancellationToken cancellationToken)
    {
        return await _productRepository.GetAllByTypeAsync(typeId, cancellationToken);
    }

    

    
}