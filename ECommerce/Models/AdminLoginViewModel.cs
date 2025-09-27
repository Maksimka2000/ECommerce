using Microsoft.Build.Framework;

namespace E_commerce.Models;

public class AdminLoginViewModel
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}