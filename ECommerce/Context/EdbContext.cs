using E_commerce.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Context;

public class EdbContext : IdentityDbContext<IdentityUser>
{
    public EdbContext(){    }  
    public EdbContext(DbContextOptions<EdbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<WishList> Wishlists { get; set; }
    public DbSet<ProductWeight> ProductWeights { get; set; }
    public DbSet<UserWeight> UserWeights { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }


    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<WishList>(entity =>
        {
            entity.HasKey(w => w.Id);
            entity.Property(pt => pt.Id).IsRequired().ValueGeneratedOnAdd();
            entity.ToTable("Wishlist");

            entity.HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(w => w.Product)
                .WithMany()
                .HasForeignKey(w => w.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        
        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(pt => pt.IdProductType);
            entity.Property(pt => pt.IdProductType).IsRequired().ValueGeneratedOnAdd();
            entity.Property(pt => pt.TypeName).HasMaxLength(100).IsRequired();
            entity.ToTable("ProductType");

            entity.HasData(SeedData.ProductTypes);
        });
        
        
        
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.IdProduct);
            entity.Property(p => p.IdProduct).IsRequired().ValueGeneratedOnAdd();
            entity.Property(p => p.Name).HasMaxLength(100).IsRequired();
            entity.Property(p => p.Description).HasMaxLength(500);
            entity.Property(p => p.Price).IsRequired();
            entity.Property(p => p.Availability).IsRequired();
            entity.ToTable("Product");

            entity.HasOne(p => p.ProductType)
                .WithMany(pt => pt.Products)
                .HasForeignKey(p => p.IdProductType).OnDelete(DeleteBehavior.Cascade);
            

            entity.HasData(SeedData.Products);
        });
        
        
        
        
        
        // vectors ========================================================================================
        modelBuilder.Entity<ProductWeight>(entity =>
        {
            entity.HasKey(w => new { w.IdProduct, w.IdProductType });
            entity.ToTable("ProductWeight");

            entity.HasOne(w => w.Product)
                .WithMany(p => p.Weights)
                .HasForeignKey(w => w.IdProduct)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(w => w.ProductType)
                .WithMany()
                .HasForeignKey(w => w.IdProductType)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(SeedData.ProductWeights);
        });

        modelBuilder.Entity<UserWeight>(entity =>
        {
            entity.HasKey(w => new { w.UserId, w.IdProductType });
            entity.ToTable("UserWeight");

            entity.HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(w => w.ProductType)
                .WithMany()
                .HasForeignKey(w => w.IdProductType)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // cart & orders ========================================================================================
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            entity.ToTable("CartItem");

            entity.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(o => o.FullName).HasMaxLength(100).IsRequired();
            entity.Property(o => o.Phone).HasMaxLength(30).IsRequired();
            entity.Property(o => o.City).HasMaxLength(100).IsRequired();
            entity.Property(o => o.Address).HasMaxLength(200).IsRequired();
            entity.Property(o => o.DeliveryMethod).HasMaxLength(50).IsRequired();
            entity.ToTable("Order");

            entity.HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(i => i.ProductName).HasMaxLength(100).IsRequired();
            entity.ToTable("OrderItem");

            entity.HasOne(i => i.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
        });
        
        
        //
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "2", Name = "Registered", NormalizedName = "REGISTERED" }
        );
        
        modelBuilder.Entity<IdentityUser>().HasData(
            new IdentityUser
            {
                Id = "1",
                PhoneNumber = "333333333",
                PhoneNumberConfirmed = true,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "admin"),
                SecurityStamp = Guid.NewGuid().ToString()
            }
        );
        
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
        new IdentityUserRole<string> { UserId = "1", RoleId = "1" }
        );
    }
}