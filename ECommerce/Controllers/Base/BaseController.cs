using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Identity;

namespace E_commerce.Controllers;


[Route("")]
[ApiController]
public class BaseController : Controller
{
    private const int RecommendationCount = 10; // K in KNN

    private readonly ILogger<BaseController> _logger;
    private readonly IProductService _productService;
    private readonly IRecommendationService _recommendationService;
    private readonly UserManager<IdentityUser> _userManager;

    public BaseController(ILogger<BaseController> logger, IProductService productService, IRecommendationService recommendationService, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _productService = productService;
        _recommendationService = recommendationService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        // logged in: KNN recommendations, guest: random products
        var user = await _userManager.GetUserAsync(User);
        var products = user != null
            ? await _recommendationService.RecommendAsync(user.Id, RecommendationCount)
            : await _productService.GetRandomProductsAsync(RecommendationCount, cancellationToken);
        ViewBag.RandomProducts = products;
        return View("Index");
    }


    [HttpGet("/Delivery")]
    public async Task<IActionResult> Delivery(CancellationToken cancellationToken)
    {
        return View("Delivery");
    }

    // the full KNN calculation: user vector, distance to every product, top K
    [HttpGet("/recommendations")]
    public async Task<IActionResult> Recommendations()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Redirect("/account/login");

        ViewBag.UserVector = await _recommendationService.GetUserVectorAsync(user.Id);
        ViewBag.K = RecommendationCount;
        var ranked = await _recommendationService.RankProductsAsync(user.Id);
        return View("Recommendations", ranked);
    }

    // settings: "show weights" toggle (user and admin)
    [HttpPost("/settings/show-weights")]
    public IActionResult ToggleShowWeights()
    {
        var isOn = Request.Cookies[ControllerExtensions.ShowWeightsCookie] == "1";
        Response.Cookies.Append(ControllerExtensions.ShowWeightsCookie, isOn ? "0" : "1",
            new CookieOptions { Expires = DateTimeOffset.Now.AddYears(1), HttpOnly = true });

        TempData[ControllerExtensions.SkipVectorUpdate] = true;
        return this.RedirectBack();
    }

}
