using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models;

public class CreateProductTypeViewModel
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
    public string TypeName { get; set; }
}