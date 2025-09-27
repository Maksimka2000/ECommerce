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
public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;
    private readonly IRecommendationService _recommendationService;
    private readonly UserManager<IdentityUser> _userManager;

    public CartController(ICartService cartService, IOrderService orderService, IRecommendationService recommendationService, UserManager<IdentityUser> userManager)
    {
        _cartService = cartService;
        _orderService = orderService;
        _recommendationService = recommendationService;
        _userManager = userManager;
    }

    [HttpGet("cart")]
    public async Task<IActionResult> Cart()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var cart = await _cartService.GetCartAsync(user.Id);
        return View("Cart", cart);
    }

    [HttpPost("cart/add")]
    public async Task<IActionResult> AddToCart([FromForm] int productId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        await _cartService.AddToCartAsync(user.Id, productId);
        await _recommendationService.ApplyProductActionAsync(user.Id, productId, VectorAction.AddToCart);

        TempData[ControllerExtensions.SkipVectorUpdate] = true;
        TempData[ControllerExtensions.OpenCart] = true;
        return this.RedirectBack();
    }

    [HttpPost("cart/remove")]
    public async Task<IActionResult> RemoveFromCart([FromForm] int productId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        try
        {
            await _cartService.RemoveFromCartAsync(user.Id, productId);
            await _recommendationService.ApplyProductActionAsync(user.Id, productId, VectorAction.RemoveFromCart);
        }
        catch (InvalidOperationException)
        {
            // already removed (e.g. double click) - nothing to do
        }

        TempData[ControllerExtensions.SkipVectorUpdate] = true;
        return this.RedirectBack("/account/cart");
    }

    [HttpGet("checkout")]
    public async Task<IActionResult> Checkout()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var cart = await _cartService.GetCartAsync(user.Id);
        if (cart.Count == 0) return RedirectToAction("Cart");

        ViewBag.Cart = cart;
        return View("Checkout", new CheckoutViewModel { FullName = user.UserName, Phone = user.PhoneNumber });
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromForm] CheckoutViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var cart = await _cartService.GetCartAsync(user.Id);
        if (cart.Count == 0) return RedirectToAction("Cart");

        if (!ModelState.IsValid)
        {
            ViewBag.Cart = cart;
            return View("Checkout", model);
        }

        await _orderService.PlaceOrderAsync(user.Id, model);

        // finalize the order: x2 for every ordered product
        await _recommendationService.ApplyProductsActionAsync(user.Id, cart.Select(c => c.ProductId), VectorAction.FinalizeOrder);

        TempData["OrderSuccess"] = "Thank you! Your order is placed. Delivery is on its way (fake delivery).";
        return Redirect("/account?tab=orders");
    }
}
