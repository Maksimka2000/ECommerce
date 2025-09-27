using E_commerce.Entities;
using E_commerce.Models;
using ECommerce.Repositories;
using ECommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace E_commerce.Controllers.Admin;

[Route("admin/product-types")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminProductTypeController : Controller
{
    
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IProductTypeService _productTypeService;
    public AdminProductTypeController(IProductTypeService productTypeService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _productTypeService = productTypeService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public async Task<IActionResult> ProductTypes(CancellationToken cancellationToken)
    {
        var productTypes = await _productTypeService.GetAllAsync(cancellationToken);
        return View("AdminProductTypes", productTypes);
    }
    
    [HttpGet("remove/{id}")]
    public async Task<IActionResult> RemoveProductType(int id, CancellationToken cancellationToken)
    {
        await _productTypeService.DeleteAsync(id, cancellationToken);
        return RedirectToAction("ProductTypes");
    }
    
    [HttpGet("add")]
    public async Task<IActionResult> AddProductType(CancellationToken cancellationToken)
    {
        return View("AdminProductTypesAdd");
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductTypeViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View("AdminProductTypesAdd", model );
        }
        await _productTypeService.AddAsync(model, cancellationToken);
        return RedirectToAction("ProductTypes");
    }
    
    [HttpGet("update/{id}")]
    public async Task<IActionResult> UpdateProductType([FromRoute] int id, CancellationToken cancellationToken)
    {
        ProductType productType = await _productTypeService.GetByIdAsync(id, cancellationToken);
        if (productType == null)
        {
            return RedirectToAction("ProductTypes"); // not found??
        }
        
        return View("AdminProductTypesUpdate", productType);
    }
    
    [HttpPost("update/{id}")]
    public async Task<IActionResult> UpdateProductType([FromRoute] int id, [FromForm] CreateProductTypeViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            ViewBag.Errors = errors;
            return View("AdminProductsUpdate", model );
        }
        
        await _productTypeService.UpdateAsync(id, model, cancellationToken);
        return RedirectToAction("ProductTypes");
    }
}