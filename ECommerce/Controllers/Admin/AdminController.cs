using E_commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers.Admin;




[Route("admin")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    
    public AdminController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> AdminPage()
    {
        return View("AdminPage"); 
    }
    
    [HttpGet("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login()
    {
        return View("AdminLogin");
    }
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm] AdminLoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("AdminLogin", model);
        }
        
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null || !(await _userManager.IsInRoleAsync(user, "Admin")))
        {
            ViewBag.ErrorText = "Invalid login attempt or insufficient permissions.";
            return View("AdminLogin", model);
        }
        
        await _signInManager.SignOutAsync();
        
        var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
        
        if (result.Succeeded)
        {
            return RedirectToAction("AdminPage");
        }
        
        ViewBag.ErrorText = "Invalid login attempt.";
        return View("AdminLogin", model);
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync(); 
        return RedirectToAction("Login"); 
    }
    
    
    
}