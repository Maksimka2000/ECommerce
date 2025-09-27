using E_commerce.Context;
using ECommerce.Repositories;
using ECommerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


// Add services to the container.
builder.Services.AddControllersWithViews();

// DI 
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddTransient<IProductTypeService, ProductTypeService>();
builder.Services.AddTransient<IWishlistRepository, WishlistRepository>();
builder.Services.AddTransient<IWishlistService, WishlistService>();
builder.Services.AddTransient<IVectorRepository, VectorRepository>();
builder.Services.AddTransient<IRecommendationService, RecommendationService>();
builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderService, OrderService>();

// add db connection

//simple
// builder.Services.AddDbContext<EdbContext>(options => options.UseSqlite("Data Source=app.db"));

// default 
// builder.Services.AddDbContext<EdbContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// readable folder in azure (without read only) it is stored in C:\Users\<YourUsername>\AppData\Local\app.db user’s Local Application Data folder.
var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "app.db");
builder.Services.AddDbContext<EdbContext>(options => options.UseSqlite($"Data Source={dbPath}"));





// identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
{
    option.User.RequireUniqueEmail = true;
    option.Password.RequiredLength = 3;
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<EdbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(option =>
{
    option.Cookie.Name = "Auth";
    option.Cookie.HttpOnly = true;
    option.LoginPath = "/account/login"; 
    option.AccessDeniedPath = "/account/denied";
    option.SlidingExpiration = true;
    
    // Admin-specific settings
    option.Events.OnRedirectToLogin = context =>
    {
        var requestPath = context.Request.Path;
        if (requestPath.StartsWithSegments("/admin"))
        {
            context.Response.Redirect("/admin/login");
        }
        if (requestPath.StartsWithSegments("/account"))
        {
            if (context.Request.Path.Equals("/account/signup", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.Redirect("/account/signup");
            }
            else
            {
                context.Response.Redirect("/account/login"); 
            }
        }
        return Task.CompletedTask;
    };
});






var app = builder.Build();

// automigration
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EdbContext>();
    db.Database.Migrate(); // applies migrations at startup
    await UserSeeder.SeedAsync(scope.ServiceProvider); // pre-created users with generated vectors
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Base}/{action=Index}");

app.Run();