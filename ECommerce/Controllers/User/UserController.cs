using E_commerce.Context;
using E_commerce.Controllers;
using E_commerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers.User;



[Route("account")]
[ApiController]
[Authorize(Roles = "Registered")]
public class UserController : Controller
{
    private readonly IWishlistService _wishlistService;
    private readonly IOrderService _orderService;
    private readonly IRecommendationService _recommendationService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    
    public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IWishlistService wishlistService, IOrderService orderService, IRecommendationService recommendationService)
    {
        _wishlistService = wishlistService;
        _orderService = orderService;
        _recommendationService = recommendationService;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    // account page with tabs: orders (order history), wishlist, settings
    [HttpGet]
    public async Task<IActionResult> UserPage([FromQuery] string tab = "orders")
    {
        var user = await _userManager.GetUserAsync(User); 

        if (user == null) return Unauthorized();
        
        if (user != null)
        {
            ViewBag.UserName = user.UserName;
        }
        
        // wishlist========================================================================================
        var wishlist = await _wishlistService.GetWishlistAsync(user.Id);
        ViewBag.ProductsInWishList = wishlist;
        //========================================================================================
        
        ViewBag.Tab = tab;
        ViewBag.Orders = await _orderService.GetOrdersAsync(user.Id);
        ViewBag.ShowWeights = Request.Cookies[ControllerExtensions.ShowWeightsCookie] == "1";
        
        return View("UserPage"); 
    }
    
    // [HttpGet("/wishlist")]
    // public async Task<IActionResult> GetWishlist()
    // {
    //     var user = await _userManager.GetUserAsync(User);
    //
    //     if (user == null) return Unauthorized();
    //
    //     var wishlist = await _wishlistService.GetWishlistAsync(user.Id);
    //     
    //     ViewBag.ProductsInWishList = wishlist;
    //
    //     return View("UserPage");
    // }
    
    
    
    [HttpPost("add-to-wishlist")]
    public async Task<IActionResult> AddToWishlist([FromForm] int productId)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null) return Unauthorized();

        try
        {
            await _wishlistService.AddToWishlistAsync(user.Id, productId);
            await _recommendationService.ApplyProductActionAsync(user.Id, productId, VectorAction.AddToWishlist);
            ViewBag.SuccessTextWishingList =  "Product successfully added to your wishlist!";
        }
        catch (InvalidOperationException ex)
        {
            ViewBag.ErrorTextWishingList =  ex.Message;
        }

        TempData[ControllerExtensions.SkipVectorUpdate] = true;
        return RedirectToAction("GetProduct", "BaseProducts", new { id = productId });
    }

    [HttpPost("remove-from-wishlist")]
    public async Task<IActionResult> RemoveFromWishlist([FromForm] int productId)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null) return Unauthorized();

        try
        {
            await _wishlistService.RemoveFromWishlistAsync(user.Id, productId);
            await _recommendationService.ApplyProductActionAsync(user.Id, productId, VectorAction.RemoveFromWishlist);
            ViewBag.SuccessTextWishingList = "Product successfully removed from your wishlist!";
        }
        catch (InvalidOperationException ex)
        {
            ViewBag.ErrorTextWishingList = ex.Message;
        }

        TempData[ControllerExtensions.SkipVectorUpdate] = true;
        return RedirectToAction("GetProduct", "BaseProducts", new { id = productId });
    }
    
    
    
    
    
    
    
    
    
    
    //
    
    [HttpGet("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login()
    {
        return View("UserLogin");
    }
    
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm] AccountLoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("UserLogin", model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return RedirectToAction("Signup");
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
        if (result.Succeeded)
        {
            return RedirectToAction("UserPage");
        }

        ViewBag.ErrorText = "Invalid login attempt. Try again";
        return View("UserLogin", model);
    }
    
    
    [HttpGet("signup")]
    [AllowAnonymous]
    public IActionResult Signup()
    {
        return View("UserSignup");
    }
    
    [HttpPost("signup")]
    [AllowAnonymous]
    public async Task<IActionResult> Signup([FromForm] AccountRegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("UserSignup", model);
        }
    
        if (model.Password != model.PasswordRepeat)
        {
            ViewBag.ErrorText =  "Passwords do not match.";
            return View("UserSignup", model);
        }
    
        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
        {
            ViewBag.ErrorText = "Username is already taken.";
            return View("UserSignup", model);
        }
        
        
        var user = new IdentityUser
        {
            PhoneNumber = model.PhoneNumber,
            PhoneNumberConfirmed = true,
            UserName = model.UserName,
            NormalizedUserName = model.UserName.ToUpper(),
            Email = model.Email,
            NormalizedEmail = model.Email.ToUpper(),
            EmailConfirmed = true,
            PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, model.Password),
            SecurityStamp = Guid.NewGuid().ToString()
        };
        
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Registered");
            await _recommendationService.CreateRandomUserVectorAsync(user.Id); // random 0-2 for every category
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("UserPage");
        }
    
        foreach (var error in result.Errors)
        {
            ViewBag.ErrorText += error.Description;
        }
    
        return View("UserSignup", model);
    }
    
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync(); 
        return RedirectToAction("Login"); 
    }
    
}