using Microsoft.AspNetCore.Identity;

namespace E_commerce.Entities;

// One cell of a user vector: how much the user is interested in a category (0-10)
public class UserWeight
{
    public string UserId { get; set; }
    public virtual IdentityUser User { get; set; }

    public int IdProductType { get; set; }
    public virtual ProductType ProductType { get; set; }

    public double Weight { get; set; }

    // how much the last action changed this weight (+ added, - removed, 0 = not changed)
    public double LastChange { get; set; }
}
