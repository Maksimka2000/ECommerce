using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models;

public class AccountRegisterViewModel
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Invalid phone number.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "User name is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "User name must be between 3 and 50 characters.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$", 
        ErrorMessage = "Password must contain at least one uppercase letter, one digit, and one special character.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Please confirm your password.")]
    [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
    public string PasswordRepeat { get; set; }
}