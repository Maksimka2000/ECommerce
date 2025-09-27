using ECommerce.Services;
using Microsoft.AspNetCore.Identity;

namespace E_commerce.Context;

// Pre-created users. Each one gets a generated (random 0-2) vector, the same as a new registration.
public static class UserSeeder
{
    public const string Password = "user";

    private static readonly (string UserName, string Email)[] Users =
    {
        ("anna", "anna@example.com"),
        ("ben", "ben@example.com"),
        ("chris", "chris@example.com")
    };

    public static async Task SeedAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var recommendationService = services.GetRequiredService<IRecommendationService>();

        foreach (var (userName, email) in Users)
        {
            if (await userManager.FindByEmailAsync(email) != null) continue;

            var user = new IdentityUser
            {
                UserName = userName,
                Email = email,
                EmailConfirmed = true,
                PhoneNumber = "333333333",
                PhoneNumberConfirmed = true
            };

            var result = await userManager.CreateAsync(user, Password);
            if (!result.Succeeded) continue;

            await userManager.AddToRoleAsync(user, "Registered");
            await recommendationService.CreateRandomUserVectorAsync(user.Id);
        }
    }
}
