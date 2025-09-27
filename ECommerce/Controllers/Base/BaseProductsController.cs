using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Identity;

namespace E_commerce.Controllers;


[Route("")]
[ApiController]
public class BaseProductsController : Controller
{
    private readonly ILogger<BaseController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IProductService _productService;
    private readonly IWishlistService _wishlistService;
    private readonly IRecommendationService _recommendationService;
    
    private const string LastCategoryCookie = "LastCategory";

    public BaseProductsController(ILogger<BaseController> logger, IProductService productService, UserManager<IdentityUser> userManager, IWishlistService wishlistService, IRecommendationService recommendationService)
    {
        _logger = logger;
        _recommendationService = recommendationService;
        _wishlistService = wishlistService;
        _userManager = userManager;
        _productService = productService;
    }

    [HttpGet("/products")]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken, int page = 1)
    {
        int pageSize = 8; 
        
        var products = await _productService.GetPaginatedProductsAsync(page, pageSize, cancellationToken);
        var totalProducts = await _productService.GetAllAsync(cancellationToken);
        int totalPages = (int)Math.Ceiling((double)totalProducts.Count() / pageSize);
        
        
        ViewData["CurrentPage"] = page;
        ViewData["TotalPages"] = totalPages;
        
        ViewBag.Products = products; 
        ViewBag.TotalProducts = totalProducts.Count();
        return View("BaseProducts");
    }
    
    [HttpGet("products/{typeId}")]
    public async Task<IActionResult> GetProductsByType(int typeId, CancellationToken cancellationToken, int page = 1)
    {
        int pageSize = 8; 
        
        // vector: entering a category (only the first page counts as entering)
        var user = await _userManager.GetUserAsync(User);
        if (user != null && page == 1 && !IsVectorUpdateSkipped())
        {
            var lastCategory = Request.Cookies[LastCategoryCookie];
            var categoryChanged = lastCategory != null && lastCategory != typeId.ToString();
            await _recommendationService.ApplyCategoryVisitAsync(user.Id, typeId, categoryChanged);
            Response.Cookies.Append(LastCategoryCookie, typeId.ToString());
        }
        
        var products = await _productService.GetPaginatedByTypeAsync(typeId, page, pageSize, cancellationToken);
        var totalProducts = await _productService.GetAllByTypeAsync(typeId, cancellationToken);
        int totalPages = (int)Math.Ceiling((double)totalProducts.Count() / pageSize);

        
        ViewData["CurrentPage"] = page;
        ViewData["TotalPages"] = totalPages;
        
        ViewBag.TypeId = typeId;
        ViewBag.Products = products;
        ViewBag.TotalProducts = totalProducts.Count();
        ViewBag.TypeName = totalProducts.ElementAt(0).ProductType.TypeName;
        return View("BaseProductsByType");
    }
    
    [HttpGet("product/{id}")]
    public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByIdAsync(id, cancellationToken);
        if (product == null) return Redirect("/products");
        
        var user = await _userManager.GetUserAsync(User);
        
        // vector: opening a product page (x1)
        if (user != null && !IsVectorUpdateSkipped())
        {
            await _recommendationService.ApplyProductActionAsync(user.Id, id, VectorAction.ViewProduct);
        }
        ViewBag.ProductVector = await _recommendationService.GetProductVectorAsync(id);
        
        // logged in: KNN recommendations, guest: random products
        var products = user != null
            ? await _recommendationService.RecommendAsync(user.Id, 10)
            : await _productService.GetRandomProductsAsync(10, cancellationToken);
        ViewBag.RandomProducts = products; 
        
        
        // wishlist ========================================================================================
        var isInWishlist = false;
        if (user != null)
        {
            isInWishlist = await _wishlistService.WishlistItemExistsAsync(user.Id, id);
        }
        ViewBag.IsInWishlist = isInWishlist;
        //========================================================================================
        
        return View("BaseProductsProduct", product);
    }
    
    // set after an action (cart, wishlist, settings) that redirects back, so the reload does not count as a new visit
    private bool IsVectorUpdateSkipped()
    {
        return TempData[ControllerExtensions.SkipVectorUpdate] is true;
    }
    
    
    
  
}