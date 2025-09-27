using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models;

public class CreateProductViewModel
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, 10000.00, ErrorMessage = "Price must be between $0.01 and $10,000.")]
    public decimal Price { get; set; }
    public bool Availability { get; set; }

    [Required(ErrorMessage = "Category is required.")]
    public int IdProductType { get; set; }
    
    public IFormFile? Photo { get; set; } 

    public string? PhotoName { get; set; } 
}