using E_commerce.Entities;
using E_commerce.Models;
using ECommerce.Repositories;
using ECommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace E_commerce.Controllers.Admin;

[Route("admin/products")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminProductController : Controller
{
    
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IProductService _productService;
    private readonly IProductTypeService _productTypeService;
    public AdminProductController(IProductService productService, IProductTypeService productTypeService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _productService = productService;
        _productTypeService = productTypeService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public async Task<IActionResult> Products(CancellationToken cancellationToken)
    {
        var products = await _productService.GetAllAsync(cancellationToken);
        return View("AdminProducts", products);
    }
    
    [HttpGet("remove/{id}")]
    public async Task<IActionResult> RemoveProduct(int id, CancellationToken cancellationToken)
    {
        await _productService.DeleteAsync(id, cancellationToken);
        return RedirectToAction("Products");
    }    
    
    [HttpGet("add")]
    public async Task<IActionResult> CreateProduct(CancellationToken cancellationToken)
    {
        var productTypes = await _productTypeService.GetAllAsync(cancellationToken);
        ViewBag.ProductTypes = productTypes;
        return View("AdminProductsAdd");
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ProductTypes = await _productTypeService.GetAllAsync(cancellationToken); 
            return View("AdminProductsAdd", model );
        }
        
        string? fileName = null;
        if (model.Photo != null && model.Photo.Length > 0)
        {
            try
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
                
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                
                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.Photo.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream); 
                }

                fileName = uniqueFileName; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
                ModelState.AddModelError("Photo", "An error occurred while saving the file.");
                ViewBag.ProductTypes = await _productTypeService.GetAllAsync(cancellationToken);
                return View("AdminProductsAdd", model);
            }
        }
        model.PhotoName = fileName;

        await _productService.AddAsync(model, cancellationToken);
        return RedirectToAction("Products");
    }
    
    [HttpGet("update/{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] int id, CancellationToken cancellationToken)
    {
        var productTypes = await _productTypeService.GetAllAsync(cancellationToken);
        ViewBag.ProductTypes = productTypes;
        
        Product product = await _productService.GetByIdAsync(id, cancellationToken);
        if (product == null)
        {
            return RedirectToAction("Products"); // not found??
        }
        
        return View("AdminProductsUpdate", product);
    }
    
    [HttpPost("update/{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromForm] CreateProductViewModel model, CancellationToken cancellationToken)
    {
        
        
        var photoName = await _productService.GetPhotoNameByProductIdAsync(id, cancellationToken);
        
        
        if (!ModelState.IsValid)
        {
            
            var errors = ModelState
                .Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            ViewBag.Errors = errors;
            
            ViewBag.ProductTypes = await _productTypeService.GetAllAsync(cancellationToken);
            model.PhotoName = photoName;
            return View("AdminProductsUpdate", model );
        }
        
        
        
        string? fileName = photoName;
        
        if (model.Photo != null && model.Photo.Length > 0)
        {
            try
            {
                // Delete the old image if exists
                if (!string.IsNullOrEmpty(fileName))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
            
                // Save the new image
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
    
                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.Photo.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
    
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }
    
                fileName = uniqueFileName; // Set the new image name
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
                ModelState.AddModelError("Photo", "An error occurred while saving the file.");
                ViewBag.ProductTypes = await _productTypeService.GetAllAsync(cancellationToken);
                return View("AdminProductsUpdate", model);
            }
        }
        
        model.PhotoName = fileName;
    
        await _productService.UpdateAsync(id, model, cancellationToken);
        return RedirectToAction("Products");
    }
}