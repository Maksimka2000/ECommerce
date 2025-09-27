namespace E_commerce.Entities;

public class ProductType
{
    public int IdProductType { get; set; }
    public string TypeName { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}