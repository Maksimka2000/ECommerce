using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models;

public class CheckoutViewModel
{
    [Required(ErrorMessage = "Full name is required.")]
    [MaxLength(100)]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Phone is required.")]
    [MaxLength(30)]
    public string Phone { get; set; }

    [Required(ErrorMessage = "City is required.")]
    [MaxLength(100)]
    public string City { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [MaxLength(200)]
    public string Address { get; set; }

    [Required(ErrorMessage = "Choose a delivery method.")]
    [MaxLength(50)]
    public string DeliveryMethod { get; set; }
}
