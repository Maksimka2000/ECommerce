# E-Commerce Application

A dynamic e-commerce platform designed to provide users with an intuitive shopping experience. The application offers a product catalog, wishlist functionality, personalized product recommendations, and an admin panel for managing products and categories. Built with ASP.NET Core MVC.

---

## Table of Contents
- [A. Description of the application (general)](#a-description-of-the-application-general)
- [C. Analytical part (problem domain, requirements, stakeholders)](#c-analytical-part-problem-domain-requirements-stakeholders)
- [D. Design part — solution and implementation details](#d-design-part--solution-and-implementation-details)
- [E. How to start the project (developer instructions)](#e-how-to-start-the-project-developer-instructions)
- [F. What to improve next (suggestions)](#f-what-to-improve-next-suggestions)
- [G. Author, Contact & License](#g-author-contact--license)
- [Acknowledgements](#acknowledgements)

---

A. Description of the application (general)
------------------------------------------
The E-Commerce Application provides a platform for browsing products, managing a wishlist, and exploring personalized product recommendations. It is designed with a user-friendly interface and a robust backend for efficient product and category management.

Key user capabilities:
- Browse a catalog of products.
- View personalized product recommendations.
- Register and log in to manage a wishlist.
- Explore products by categories.
- Access detailed product pages to view descriptions, images, and other key information.

Admin capabilities:
- Use the admin panel to add, update, or remove products and categories.

---

C. Analytical part (problem domain, requirements, stakeholders)
---------------------------------------------------------------
Business reality and domain:
- The platform is designed for users to explore and manage products, while administrators oversee inventory and product categorization.
- Data must support relationships such as products belonging to categories and users maintaining wishlists.

Functional requirements:
1. User functionalities:
   - A comprehensive product catalog, allowing users to browse all products.
   - Personalized product recommendations based on user preferences or popular items.
   - Dedicated product pages with detailed information, including images and descriptions.
   - Log in and register functionality to access personalized features like the wishlist.
   - Register, log in, and maintain a wishlist.
   - View personalized product recommendations.
   - Users can create accounts to maintain a wishlist of their favorite products.
   - Browse products by specific categories.
2. Admin functionalities:
   - CRUD operations for products and categories through the admin panel.
   - Manage products and categories with the ability to add, update, or delete them.
3. UI pages:
   - Product catalog, product details, login, registration, and admin management pages.

Stakeholders:
- Customers (end users).
- Store administrators (content managers).
- Developers/maintainers.

---

D. Design part — solution and implementation details
----------------------------------------------------

**Technology stack**
- .NET runtime: .NET 6/7 (recommend using the latest supported SDK).
- ASP.NET Core MVC with Razor view engine (.cshtml).
- Entity Framework Core (Code-First) with SQL Server for database management.
- Frontend: HTML5, CSS, JavaScript (vanilla or libraries as needed).

---

**Project structure**
- /Controllers
  - HomeController.cs (main pages)
  - AccountController.cs (auth: Register, Login, Logout)
  - AdminController.cs (admin dashboard and management pages)
  - ProductController.cs (product catalog and details)
  - WishlistController.cs (wishlist management)
- /Models
  - Product.cs
  - Category.cs
  - Wishlist.cs
  - ViewModels (e.g., ProductCreateEditViewModel, WishlistViewModel)
- /Data
  - AppDbContext.cs (inherits IdentityDbContext<IdentityUser>)
  - Migrations/...
- /Services
  - ProductService.cs (business logic)
  - WishlistService.cs
- /Repositories
  - ProductRepository.cs
  - CategoryRepository.cs
  - GenericRepository.cs
- /Views
  - /Shared/_Layout.cshtml
  - /Home, /Account, /Admin, /Products, /Wishlist
- /wwwroot
  - /css, /js, /images

---

**Authentication & Authorization**
- Uses ASP.NET Core Identity for user and role management.
- Authentication via cookies for secure user sessions.
- Authorization policies with roles ("Admin" and "Customer").

---

**Data model and EF Core (Code-First)**
- DbContext: AppDbContext : IdentityDbContext<IdentityUser>
  - DbSet<Product> Products
  - DbSet<Category> Categories
  - DbSet<Wishlist> Wishlists
- Seed data for demo purposes:
  - Sample products, categories, and an admin user.

---

E. How to start the project (developer instructions)
----------------------------------------------------

Prerequisites:
- .NET SDK (6 or 7 recommended): https://dotnet.microsoft.com/download
- SQL Server (local or cloud-hosted).

Quick start:
1. Clone the repository:
   ```bash
   git clone https://github.com/Maksimka2000/ECommerce.git
   cd ECommerce
   ```
2. Restore packages and run:
   ```bash
   dotnet restore
   dotnet run
   ```
3. Open your browser to the URL shown in the console (e.g., https://localhost:5001).

Database setup:
- The application uses SQL Server. Update the connection string in `appsettings.json` as needed.
- Apply migrations to create the database schema:
  ```bash
  dotnet ef database update
  ```

Admin access:
- A default admin user/role is seeded for demo purposes. Check the seed code for the default credentials.

---

F. What to improve next (suggestions)
------------------------------------
- Add advanced filtering options for the product catalog.
- Enhance product recommendation algorithms.
- Introduce customer feedback and ratings on product pages.
- Add unit and integration tests for key components.
- Implement payment gateway integration for a full e-commerce experience.
- Optimize performance for large product catalogs.

---

G. Author, Contact & License
-------------------------
- Author / Repository: Maksimka2000 — https://github.com/Maksimka2000/ECommerce
- Contact email: skrypka_maks@ukr.net
- License: MIT (add LICENSE file to repo)

---

Acknowledgements
- Built with ASP.NET Core, Entity Framework Core, Razor.
- Icons and images from free online resources.

---
