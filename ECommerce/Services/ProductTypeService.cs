using E_commerce.Entities;
using E_commerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services;

public class ProductTypeService : IProductTypeService
{
    private readonly IProductTypeRepository _productTypeRepository;

    public ProductTypeService(IProductTypeRepository productTypeRepository)
    {
        _productTypeRepository = productTypeRepository;
    }

    public IEnumerable<ProductType> GetAllAsync()
    {
        return  _productTypeRepository.GetAllAsync();
    }
    
    public async Task<IEnumerable<ProductType>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _productTypeRepository.GetAllAsync(cancellationToken);
    }

    public async Task<ProductType> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _productTypeRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task AddAsync(CreateProductTypeViewModel model, CancellationToken cancellationToken)
    {
        
        var productType = new ProductType()
        {
            TypeName = model.TypeName,
        };
        
        await _productTypeRepository.AddAsync(productType, cancellationToken);
    }

    public async Task UpdateAsync(int id, CreateProductTypeViewModel model, CancellationToken cancellationToken)
    {
        
        var productType = new ProductType()
        {
            IdProductType = id,
            TypeName = model.TypeName,
        };
        
        await _productTypeRepository.UpdateAsync(productType, cancellationToken);
    }

    public async Task DeleteAsync(int id,  CancellationToken cancellationToken)
    {
        var productType = await GetByIdAsync(id, cancellationToken);
        if (productType != null)
        {
            await _productTypeRepository.DeleteAsync(productType, cancellationToken);
        }
        
    }
}