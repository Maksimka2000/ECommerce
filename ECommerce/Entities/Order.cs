using Microsoft.AspNetCore.Identity;

namespace E_commerce.Entities;

public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public virtual IdentityUser User { get; set; }
    public DateTime CreatedAt { get; set; }

    // fake delivery data
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string DeliveryMethod { get; set; }

    public decimal Total { get; set; }
    public virtual ICollection<OrderItem> Items { get; set; }
}
