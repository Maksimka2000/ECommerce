namespace E_commerce.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }

    // product can be deleted by admin later, so name and price are copied into the order
    public int? ProductId { get; set; }
    public virtual Product? Product { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
