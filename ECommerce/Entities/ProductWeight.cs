namespace E_commerce.Entities;

// One cell of a product vector: how much the product belongs to a category (0-10)
public class ProductWeight
{
    public int IdProduct { get; set; }
    public virtual Product Product { get; set; }

    public int IdProductType { get; set; }
    public virtual ProductType ProductType { get; set; }

    public double Weight { get; set; }
}
