namespace E_commerce.Entities;

public class Product
{
    public int IdProduct { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Availability { get; set; }
    public decimal Price { get; set; }
    public string? PhotoName { get; set; }

    // Foreign Key
    public int IdProductType { get; set; }
    public virtual ProductType ProductType { get; set; }

    // product vector (one weight per category)
    public virtual ICollection<ProductWeight> Weights { get; set; }
    
   
}