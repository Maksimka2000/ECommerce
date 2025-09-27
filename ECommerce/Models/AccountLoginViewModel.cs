using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models;

public class AccountLoginViewModel
{
    [Required(ErrorMessage = "User name is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one digit, and one special character.")]
    public string Password { get; set; }
}