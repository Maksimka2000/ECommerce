using Microsoft.AspNetCore.Identity;

namespace E_commerce.Entities;

public class WishList
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public virtual IdentityUser User { get; set; }
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
}