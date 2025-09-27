using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class VectorsCartOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItem_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DeliveryMethod = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductWeight",
                columns: table => new
                {
                    IdProduct = table.Column<int>(type: "INTEGER", nullable: false),
                    IdProductType = table.Column<int>(type: "INTEGER", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWeight", x => new { x.IdProduct, x.IdProductType });
                    table.ForeignKey(
                        name: "FK_ProductWeight_ProductType_IdProductType",
                        column: x => x.IdProductType,
                        principalTable: "ProductType",
                        principalColumn: "IdProductType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductWeight_Product_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "Product",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWeight",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    IdProductType = table.Column<int>(type: "INTEGER", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWeight", x => new { x.UserId, x.IdProductType });
                    table.ForeignKey(
                        name: "FK_UserWeight_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWeight_ProductType_IdProductType",
                        column: x => x.IdProductType,
                        principalTable: "ProductType",
                        principalColumn: "IdProductType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProductName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0675a3d-4440-4f2a-b670-2a92f2f21770", "AQAAAAIAAYagAAAAEALmSOUzLMIupAAytUF7HxNOjUGNuF4d7vIXSvOwsHZh0VgmVMsxOy1RZ6nYA7m/+Q==", "c3b71cc7-e507-43d7-b16c-53c6f61fb87b" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "IdProduct", "Availability", "Description", "IdProductType", "Name", "PhotoName", "Price" },
                values: new object[,]
                {
                    { 9, true, "Pair of adjustable dumbbells for home workouts.", 1, "Dumbbell Set 20kg", "bar.jpg", 89.00m },
                    { 10, true, "Cast iron kettlebell for swings and squats.", 1, "Kettlebell 16kg", "no-photo.svg", 49.00m },
                    { 11, true, "Foldable bench with 6 incline positions.", 1, "Adjustable Bench", "no-photo.svg", 159.00m },
                    { 12, true, "Doorway pull-up bar, no screws needed.", 1, "Pull-up Bar", "no-photo.svg", 29.99m },
                    { 13, true, "Five bands with different resistance levels.", 1, "Resistance Bands Set", "no-photo.svg", 24.50m },
                    { 14, true, "Rubber coated plates for a standard bar.", 1, "Weight Plates 2x10kg", "bar.jpg", 69.00m },
                    { 15, true, "Padded gloves with wrist support.", 1, "Lifting Gloves", "no-photo.svg", 19.99m },
                    { 16, true, "Spacious bag with a shoe compartment.", 1, "Gym Bag", "no-photo.svg", 39.00m },
                    { 17, true, "Firm roller for muscle recovery.", 1, "Foam Roller", "no-photo.svg", 22.00m },
                    { 18, true, "Quick-dry shorts for the pool and the beach.", 2, "Swimming Shorts", "no-photo.svg", 25.00m },
                    { 19, true, "Anti-fog goggles with UV protection.", 2, "Swim Goggles", "no-photo.svg", 14.99m },
                    { 20, true, "Silicone cap that keeps hair dry.", 2, "Swim Cap", "no-photo.svg", 9.99m },
                    { 21, true, "Foam board for leg training.", 2, "Kickboard", "no-photo.svg", 12.50m },
                    { 22, true, "Chlorine resistant one-piece swimsuit.", 2, "Swimsuit", "no-photo.svg", 39.00m },
                    { 23, true, "Short training fins for speed.", 2, "Fins", "no-photo.svg", 34.00m },
                    { 24, true, "Full face snorkel mask with a dry top.", 2, "Full Face Snorkel", "snorkel.jpg", 49.00m },
                    { 25, true, "Foam buoy for arm training.", 2, "Pull Buoy", "no-photo.svg", 11.00m },
                    { 26, true, "Light towel that dries fast.", 2, "Microfiber Towel", "no-photo.svg", 17.00m },
                    { 27, true, "Pressurized balls for all court types.", 3, "Tennis Balls (3 pack)", "teniss.jpg", 8.99m },
                    { 28, true, "Court shoes with lateral support.", 3, "Tennis Shoes", "no-photo.svg", 99.00m },
                    { 29, true, "Bag for up to 6 rackets.", 3, "Racket Bag", "no-photo.svg", 59.00m },
                    { 30, true, "Pack of 12 tacky overgrips.", 3, "Overgrip Pack", "racket.jpg", 12.00m },
                    { 31, true, "Light skirt with built-in shorts.", 3, "Tennis Skirt", "no-photo.svg", 35.00m },
                    { 32, true, "Cotton wristbands, pair.", 3, "Wristbands", "no-photo.svg", 6.50m },
                    { 33, true, "Breathable cap with a long visor.", 3, "Tennis Cap", "no-photo.svg", 18.00m },
                    { 34, true, "Portable machine for solo training.", 3, "Ball Machine", "no-photo.svg", 499.00m },
                    { 35, true, "Regulation size net with steel cable.", 3, "Tennis Net", "no-photo.svg", 129.00m },
                    { 36, true, "Classic quad skates with soft wheels.", 4, "Roller Skates", "roll.jpg", 89.00m },
                    { 37, true, "Foldable aluminium scooter.", 4, "Kick Scooter", "skate.jpg", 79.00m },
                    { 38, true, "Ultimate disc, 175g.", 4, "Frisbee", "no-photo.svg", 14.00m },
                    { 39, true, "Two rackets, net and shuttles.", 4, "Badminton Set", "no-photo.svg", 45.00m },
                    { 40, true, "Two paddles and three balls.", 4, "Table Tennis Paddles", "no-photo.svg", 29.00m },
                    { 41, true, "Insulated steel bottle, 750 ml.", 4, "Water Bottle", "no-photo.svg", 19.00m },
                    { 42, true, "GPS watch with heart rate tracking.", 4, "Sports Watch", "no-photo.svg", 199.00m },
                    { 43, true, "Steel tip darts with a board.", 4, "Darts Set", "no-photo.svg", 39.00m }
                });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "IdProductType", "TypeName" },
                values: new object[,]
                {
                    { 5, "Running" },
                    { 6, "Football" },
                    { 7, "Basketball" },
                    { 8, "Cycling" },
                    { 9, "Hiking" },
                    { 10, "Yoga" },
                    { 11, "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "ProductWeight",
                columns: new[] { "IdProduct", "IdProductType", "Weight" },
                values: new object[,]
                {
                    { 1, 1, 10.0 },
                    { 1, 2, 0.0 },
                    { 1, 3, 0.0 },
                    { 1, 4, 0.0 },
                    { 2, 1, 8.0 },
                    { 2, 2, 1.0 },
                    { 2, 3, 3.0 },
                    { 2, 4, 1.0 },
                    { 3, 1, 0.0 },
                    { 3, 2, 10.0 },
                    { 3, 3, 0.0 },
                    { 3, 4, 2.0 },
                    { 4, 1, 1.0 },
                    { 4, 2, 9.0 },
                    { 4, 3, 0.0 },
                    { 4, 4, 1.0 },
                    { 5, 1, 0.0 },
                    { 5, 2, 9.0 },
                    { 5, 3, 0.0 },
                    { 5, 4, 1.0 },
                    { 6, 1, 1.0 },
                    { 6, 2, 0.0 },
                    { 6, 3, 10.0 },
                    { 6, 4, 0.0 },
                    { 7, 1, 1.0 },
                    { 7, 2, 0.0 },
                    { 7, 3, 9.0 },
                    { 7, 4, 1.0 },
                    { 8, 1, 1.0 },
                    { 8, 2, 0.0 },
                    { 8, 3, 0.0 },
                    { 8, 4, 10.0 }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "IdProduct", "Availability", "Description", "IdProductType", "Name", "PhotoName", "Price" },
                values: new object[,]
                {
                    { 44, true, "Cushioned shoes for road running.", 5, "Running Shoes", "no-photo.svg", 119.00m },
                    { 45, true, "Light shorts with an inner pocket.", 5, "Running Shorts", "no-photo.svg", 29.00m },
                    { 46, true, "Windproof jacket with reflective details.", 5, "Running Jacket", "no-photo.svg", 79.00m },
                    { 47, true, "Vest with two soft flasks.", 5, "Hydration Vest", "no-photo.svg", 69.00m },
                    { 48, true, "Anti-blister socks, 3 pairs.", 5, "Running Socks", "no-photo.svg", 15.00m },
                    { 49, true, "Rechargeable headlamp, 400 lumen.", 5, "Headlamp", "no-photo.svg", 35.00m },
                    { 50, true, "Belt for phone and keys.", 5, "Running Belt", "no-photo.svg", 17.00m },
                    { 51, true, "Tights that support the muscles.", 5, "Compression Tights", "no-photo.svg", 49.00m },
                    { 52, true, "Chest strap with Bluetooth.", 5, "Heart Rate Monitor", "no-photo.svg", 59.00m },
                    { 53, true, "Size 5 match ball.", 6, "Football Ball", "no-photo.svg", 29.00m },
                    { 54, true, "Boots for firm ground.", 6, "Football Boots", "no-photo.svg", 109.00m },
                    { 55, true, "Light guards with ankle protection.", 6, "Shin Guards", "no-photo.svg", 19.00m },
                    { 56, true, "Gloves with latex palm.", 6, "Goalkeeper Gloves", "no-photo.svg", 39.00m },
                    { 57, true, "Breathable team jersey.", 6, "Football Jersey", "no-photo.svg", 45.00m },
                    { 58, true, "Set of 20 cones for drills.", 6, "Training Cones", "no-photo.svg", 12.00m },
                    { 59, true, "Pop-up goal for the garden.", 6, "Mini Goal", "no-photo.svg", 59.00m },
                    { 60, true, "Long socks, one size.", 6, "Football Socks", "no-photo.svg", 9.00m },
                    { 61, true, "Hand pump with needle.", 6, "Ball Pump", "no-photo.svg", 8.00m },
                    { 62, true, "Size 7 indoor/outdoor ball.", 7, "Basketball Ball", "no-photo.svg", 35.00m },
                    { 63, true, "High top shoes with ankle support.", 7, "Basketball Shoes", "no-photo.svg", 129.00m },
                    { 64, true, "Wall mounted hoop with net.", 7, "Basketball Hoop", "no-photo.svg", 149.00m },
                    { 65, true, "Sleeveless mesh jersey.", 7, "Basketball Jersey", "no-photo.svg", 39.00m },
                    { 66, true, "Long shorts with side pockets.", 7, "Basketball Shorts", "no-photo.svg", 32.00m },
                    { 67, true, "Compression sleeve, one arm.", 7, "Arm Sleeve", "no-photo.svg", 12.00m },
                    { 68, true, "Heavy duty replacement net.", 7, "Basketball Net", "no-photo.svg", 10.00m },
                    { 69, true, "Goggles that block the view of the ball.", 7, "Dribble Goggles", "no-photo.svg", 15.00m },
                    { 70, true, "Backpack with a ball net.", 7, "Basketball Bag", "no-photo.svg", 45.00m },
                    { 71, true, "Aluminium road bike, 18 speeds.", 8, "Road Bike", "no-photo.svg", 899.00m },
                    { 72, true, "Hardtail MTB with front suspension.", 8, "Mountain Bike", "no-photo.svg", 749.00m },
                    { 73, true, "Vented helmet with a rear light.", 8, "Cycling Helmet", "no-photo.svg", 69.00m },
                    { 74, true, "Front and rear USB lights.", 8, "Bike Lights", "no-photo.svg", 25.00m },
                    { 75, true, "Half finger padded gloves.", 8, "Cycling Gloves", "no-photo.svg", 22.00m },
                    { 76, true, "Jersey with three back pockets.", 8, "Cycling Jersey", "no-photo.svg", 59.00m },
                    { 77, true, "Hardened steel U-lock.", 8, "Bike Lock", "no-photo.svg", 35.00m },
                    { 78, true, "Floor pump with gauge.", 8, "Bike Pump", "no-photo.svg", 29.00m },
                    { 79, true, "Bib shorts with a padded insert.", 8, "Cycling Shorts", "no-photo.svg", 65.00m },
                    { 80, true, "40 L backpack with a rain cover.", 9, "Hiking Backpack", "no-photo.svg", 89.00m },
                    { 81, true, "Waterproof boots with a grippy sole.", 9, "Hiking Boots", "no-photo.svg", 139.00m },
                    { 82, true, "Pair of foldable aluminium poles.", 9, "Trekking Poles", "no-photo.svg", 49.00m },
                    { 83, true, "Two person tent, 2.1 kg.", 9, "Tent 2P", "no-photo.svg", 199.00m },
                    { 84, true, "Three season sleeping bag.", 9, "Sleeping Bag", "no-photo.svg", 79.00m },
                    { 85, true, "Compact gas stove.", 9, "Camping Stove", "no-photo.svg", 39.00m },
                    { 86, true, "Waterproof shell jacket.", 9, "Hiking Jacket", "no-photo.svg", 149.00m },
                    { 87, true, "Baseplate compass with a mirror.", 9, "Compass", "no-photo.svg", 19.00m },
                    { 88, true, "Light kit for the trail.", 9, "First Aid Kit", "no-photo.svg", 25.00m },
                    { 89, true, "6 mm non-slip mat.", 10, "Yoga Mat", "no-photo.svg", 35.00m },
                    { 90, true, "Two cork blocks.", 10, "Yoga Blocks", "no-photo.svg", 22.00m },
                    { 91, true, "Cotton strap with a metal buckle.", 10, "Yoga Strap", "no-photo.svg", 9.00m },
                    { 92, true, "Round cushion filled with buckwheat.", 10, "Meditation Cushion", "no-photo.svg", 39.00m },
                    { 93, true, "High waist stretch leggings.", 10, "Yoga Leggings", "no-photo.svg", 45.00m },
                    { 94, true, "Wheel for backbends and stretching.", 10, "Yoga Wheel", "no-photo.svg", 29.00m },
                    { 95, true, "65 cm anti-burst ball.", 10, "Pilates Ball", "no-photo.svg", 24.00m },
                    { 96, true, "Moisture wicking t-shirt.", 11, "Sport T-Shirt", "no-photo.svg", 19.00m },
                    { 97, true, "Warm cotton hoodie.", 11, "Hoodie", "no-photo.svg", 49.00m },
                    { 98, true, "Tapered pants with zip pockets.", 11, "Track Pants", "no-photo.svg", 39.00m },
                    { 99, true, "Medium support sports bra.", 11, "Sports Bra", "no-photo.svg", 29.00m },
                    { 100, true, "Light cap for sunny days.", 11, "Sport Cap", "no-photo.svg", 15.00m }
                });

            migrationBuilder.InsertData(
                table: "ProductWeight",
                columns: new[] { "IdProduct", "IdProductType", "Weight" },
                values: new object[,]
                {
                    { 1, 5, 0.0 },
                    { 1, 6, 0.0 },
                    { 1, 7, 0.0 },
                    { 1, 8, 0.0 },
                    { 1, 9, 0.0 },
                    { 1, 10, 1.0 },
                    { 1, 11, 0.0 },
                    { 2, 5, 4.0 },
                    { 2, 6, 2.0 },
                    { 2, 7, 2.0 },
                    { 2, 8, 0.0 },
                    { 2, 9, 0.0 },
                    { 2, 10, 0.0 },
                    { 2, 11, 0.0 },
                    { 3, 5, 0.0 },
                    { 3, 6, 0.0 },
                    { 3, 7, 0.0 },
                    { 3, 8, 0.0 },
                    { 3, 9, 0.0 },
                    { 3, 10, 0.0 },
                    { 3, 11, 0.0 },
                    { 4, 5, 0.0 },
                    { 4, 6, 0.0 },
                    { 4, 7, 0.0 },
                    { 4, 8, 0.0 },
                    { 4, 9, 0.0 },
                    { 4, 10, 0.0 },
                    { 4, 11, 2.0 },
                    { 5, 5, 0.0 },
                    { 5, 6, 0.0 },
                    { 5, 7, 0.0 },
                    { 5, 8, 0.0 },
                    { 5, 9, 0.0 },
                    { 5, 10, 0.0 },
                    { 5, 11, 0.0 },
                    { 6, 5, 0.0 },
                    { 6, 6, 0.0 },
                    { 6, 7, 0.0 },
                    { 6, 8, 0.0 },
                    { 6, 9, 0.0 },
                    { 6, 10, 0.0 },
                    { 6, 11, 0.0 },
                    { 7, 5, 0.0 },
                    { 7, 6, 0.0 },
                    { 7, 7, 0.0 },
                    { 7, 8, 0.0 },
                    { 7, 9, 0.0 },
                    { 7, 10, 0.0 },
                    { 7, 11, 0.0 },
                    { 8, 5, 0.0 },
                    { 8, 6, 0.0 },
                    { 8, 7, 0.0 },
                    { 8, 8, 2.0 },
                    { 8, 9, 0.0 },
                    { 8, 10, 0.0 },
                    { 8, 11, 1.0 },
                    { 9, 1, 10.0 },
                    { 9, 2, 0.0 },
                    { 9, 3, 0.0 },
                    { 9, 4, 0.0 },
                    { 9, 5, 0.0 },
                    { 9, 6, 0.0 },
                    { 9, 7, 0.0 },
                    { 9, 8, 0.0 },
                    { 9, 9, 0.0 },
                    { 9, 10, 1.0 },
                    { 9, 11, 0.0 },
                    { 10, 1, 9.0 },
                    { 10, 2, 0.0 },
                    { 10, 3, 0.0 },
                    { 10, 4, 0.0 },
                    { 10, 5, 1.0 },
                    { 10, 6, 0.0 },
                    { 10, 7, 0.0 },
                    { 10, 8, 0.0 },
                    { 10, 9, 0.0 },
                    { 10, 10, 1.0 },
                    { 10, 11, 0.0 },
                    { 11, 1, 9.0 },
                    { 11, 2, 0.0 },
                    { 11, 3, 0.0 },
                    { 11, 4, 1.0 },
                    { 11, 5, 0.0 },
                    { 11, 6, 0.0 },
                    { 11, 7, 0.0 },
                    { 11, 8, 0.0 },
                    { 11, 9, 0.0 },
                    { 11, 10, 0.0 },
                    { 11, 11, 0.0 },
                    { 12, 1, 8.0 },
                    { 12, 2, 0.0 },
                    { 12, 3, 0.0 },
                    { 12, 4, 1.0 },
                    { 12, 5, 0.0 },
                    { 12, 6, 0.0 },
                    { 12, 7, 1.0 },
                    { 12, 8, 0.0 },
                    { 12, 9, 0.0 },
                    { 12, 10, 1.0 },
                    { 12, 11, 0.0 },
                    { 13, 1, 7.0 },
                    { 13, 2, 0.0 },
                    { 13, 3, 1.0 },
                    { 13, 4, 0.0 },
                    { 13, 5, 2.0 },
                    { 13, 6, 1.0 },
                    { 13, 7, 1.0 },
                    { 13, 8, 0.0 },
                    { 13, 9, 0.0 },
                    { 13, 10, 4.0 },
                    { 13, 11, 0.0 },
                    { 14, 1, 10.0 },
                    { 14, 2, 0.0 },
                    { 14, 3, 0.0 },
                    { 14, 4, 0.0 },
                    { 14, 5, 0.0 },
                    { 14, 6, 0.0 },
                    { 14, 7, 0.0 },
                    { 14, 8, 0.0 },
                    { 14, 9, 0.0 },
                    { 14, 10, 0.0 },
                    { 14, 11, 0.0 },
                    { 15, 1, 7.0 },
                    { 15, 2, 0.0 },
                    { 15, 3, 0.0 },
                    { 15, 4, 0.0 },
                    { 15, 5, 0.0 },
                    { 15, 6, 0.0 },
                    { 15, 7, 0.0 },
                    { 15, 8, 1.0 },
                    { 15, 9, 0.0 },
                    { 15, 10, 0.0 },
                    { 15, 11, 4.0 },
                    { 16, 1, 6.0 },
                    { 16, 2, 2.0 },
                    { 16, 3, 2.0 },
                    { 16, 4, 3.0 },
                    { 16, 5, 2.0 },
                    { 16, 6, 2.0 },
                    { 16, 7, 2.0 },
                    { 16, 8, 0.0 },
                    { 16, 9, 1.0 },
                    { 16, 10, 1.0 },
                    { 16, 11, 2.0 },
                    { 17, 1, 6.0 },
                    { 17, 2, 0.0 },
                    { 17, 3, 0.0 },
                    { 17, 4, 0.0 },
                    { 17, 5, 3.0 },
                    { 17, 6, 1.0 },
                    { 17, 7, 1.0 },
                    { 17, 8, 1.0 },
                    { 17, 9, 0.0 },
                    { 17, 10, 4.0 },
                    { 17, 11, 0.0 },
                    { 18, 1, 0.0 },
                    { 18, 2, 8.0 },
                    { 18, 3, 0.0 },
                    { 18, 4, 1.0 },
                    { 18, 5, 0.0 },
                    { 18, 6, 0.0 },
                    { 18, 7, 0.0 },
                    { 18, 8, 0.0 },
                    { 18, 9, 0.0 },
                    { 18, 10, 0.0 },
                    { 18, 11, 4.0 },
                    { 19, 1, 0.0 },
                    { 19, 2, 10.0 },
                    { 19, 3, 0.0 },
                    { 19, 4, 0.0 },
                    { 19, 5, 0.0 },
                    { 19, 6, 0.0 },
                    { 19, 7, 0.0 },
                    { 19, 8, 0.0 },
                    { 19, 9, 0.0 },
                    { 19, 10, 0.0 },
                    { 19, 11, 0.0 },
                    { 20, 1, 0.0 },
                    { 20, 2, 9.0 },
                    { 20, 3, 0.0 },
                    { 20, 4, 0.0 },
                    { 20, 5, 0.0 },
                    { 20, 6, 0.0 },
                    { 20, 7, 0.0 },
                    { 20, 8, 0.0 },
                    { 20, 9, 0.0 },
                    { 20, 10, 0.0 },
                    { 20, 11, 2.0 },
                    { 21, 1, 1.0 },
                    { 21, 2, 9.0 },
                    { 21, 3, 0.0 },
                    { 21, 4, 0.0 },
                    { 21, 5, 0.0 },
                    { 21, 6, 0.0 },
                    { 21, 7, 0.0 },
                    { 21, 8, 0.0 },
                    { 21, 9, 0.0 },
                    { 21, 10, 0.0 },
                    { 21, 11, 0.0 },
                    { 22, 1, 0.0 },
                    { 22, 2, 8.0 },
                    { 22, 3, 0.0 },
                    { 22, 4, 0.0 },
                    { 22, 5, 0.0 },
                    { 22, 6, 0.0 },
                    { 22, 7, 0.0 },
                    { 22, 8, 0.0 },
                    { 22, 9, 0.0 },
                    { 22, 10, 0.0 },
                    { 22, 11, 5.0 },
                    { 23, 1, 0.0 },
                    { 23, 2, 9.0 },
                    { 23, 3, 0.0 },
                    { 23, 4, 1.0 },
                    { 23, 5, 0.0 },
                    { 23, 6, 0.0 },
                    { 23, 7, 0.0 },
                    { 23, 8, 0.0 },
                    { 23, 9, 0.0 },
                    { 23, 10, 0.0 },
                    { 23, 11, 0.0 },
                    { 24, 1, 0.0 },
                    { 24, 2, 9.0 },
                    { 24, 3, 0.0 },
                    { 24, 4, 3.0 },
                    { 24, 5, 0.0 },
                    { 24, 6, 0.0 },
                    { 24, 7, 0.0 },
                    { 24, 8, 0.0 },
                    { 24, 9, 1.0 },
                    { 24, 10, 0.0 },
                    { 24, 11, 0.0 },
                    { 25, 1, 1.0 },
                    { 25, 2, 9.0 },
                    { 25, 3, 0.0 },
                    { 25, 4, 0.0 },
                    { 25, 5, 0.0 },
                    { 25, 6, 0.0 },
                    { 25, 7, 0.0 },
                    { 25, 8, 0.0 },
                    { 25, 9, 0.0 },
                    { 25, 10, 0.0 },
                    { 25, 11, 0.0 },
                    { 26, 1, 1.0 },
                    { 26, 2, 6.0 },
                    { 26, 3, 0.0 },
                    { 26, 4, 2.0 },
                    { 26, 5, 1.0 },
                    { 26, 6, 0.0 },
                    { 26, 7, 0.0 },
                    { 26, 8, 0.0 },
                    { 26, 9, 2.0 },
                    { 26, 10, 2.0 },
                    { 26, 11, 0.0 },
                    { 27, 1, 0.0 },
                    { 27, 2, 0.0 },
                    { 27, 3, 10.0 },
                    { 27, 4, 0.0 },
                    { 27, 5, 0.0 },
                    { 27, 6, 0.0 },
                    { 27, 7, 0.0 },
                    { 27, 8, 0.0 },
                    { 27, 9, 0.0 },
                    { 27, 10, 0.0 },
                    { 27, 11, 0.0 },
                    { 28, 1, 0.0 },
                    { 28, 2, 0.0 },
                    { 28, 3, 8.0 },
                    { 28, 4, 0.0 },
                    { 28, 5, 2.0 },
                    { 28, 6, 0.0 },
                    { 28, 7, 0.0 },
                    { 28, 8, 0.0 },
                    { 28, 9, 0.0 },
                    { 28, 10, 0.0 },
                    { 28, 11, 4.0 },
                    { 29, 1, 0.0 },
                    { 29, 2, 0.0 },
                    { 29, 3, 8.0 },
                    { 29, 4, 2.0 },
                    { 29, 5, 0.0 },
                    { 29, 6, 0.0 },
                    { 29, 7, 0.0 },
                    { 29, 8, 0.0 },
                    { 29, 9, 0.0 },
                    { 29, 10, 0.0 },
                    { 29, 11, 1.0 },
                    { 30, 1, 0.0 },
                    { 30, 2, 0.0 },
                    { 30, 3, 9.0 },
                    { 30, 4, 0.0 },
                    { 30, 5, 0.0 },
                    { 30, 6, 0.0 },
                    { 30, 7, 0.0 },
                    { 30, 8, 0.0 },
                    { 30, 9, 0.0 },
                    { 30, 10, 0.0 },
                    { 30, 11, 0.0 },
                    { 31, 1, 0.0 },
                    { 31, 2, 0.0 },
                    { 31, 3, 7.0 },
                    { 31, 4, 0.0 },
                    { 31, 5, 1.0 },
                    { 31, 6, 0.0 },
                    { 31, 7, 0.0 },
                    { 31, 8, 0.0 },
                    { 31, 9, 0.0 },
                    { 31, 10, 0.0 },
                    { 31, 11, 5.0 },
                    { 32, 1, 1.0 },
                    { 32, 2, 0.0 },
                    { 32, 3, 6.0 },
                    { 32, 4, 0.0 },
                    { 32, 5, 1.0 },
                    { 32, 6, 0.0 },
                    { 32, 7, 2.0 },
                    { 32, 8, 0.0 },
                    { 32, 9, 0.0 },
                    { 32, 10, 0.0 },
                    { 32, 11, 3.0 },
                    { 33, 1, 0.0 },
                    { 33, 2, 0.0 },
                    { 33, 3, 6.0 },
                    { 33, 4, 0.0 },
                    { 33, 5, 2.0 },
                    { 33, 6, 0.0 },
                    { 33, 7, 0.0 },
                    { 33, 8, 0.0 },
                    { 33, 9, 1.0 },
                    { 33, 10, 0.0 },
                    { 33, 11, 4.0 },
                    { 34, 1, 0.0 },
                    { 34, 2, 0.0 },
                    { 34, 3, 10.0 },
                    { 34, 4, 0.0 },
                    { 34, 5, 0.0 },
                    { 34, 6, 0.0 },
                    { 34, 7, 0.0 },
                    { 34, 8, 0.0 },
                    { 34, 9, 0.0 },
                    { 34, 10, 0.0 },
                    { 34, 11, 0.0 },
                    { 35, 1, 0.0 },
                    { 35, 2, 0.0 },
                    { 35, 3, 10.0 },
                    { 35, 4, 1.0 },
                    { 35, 5, 0.0 },
                    { 35, 6, 0.0 },
                    { 35, 7, 0.0 },
                    { 35, 8, 0.0 },
                    { 35, 9, 0.0 },
                    { 35, 10, 0.0 },
                    { 35, 11, 0.0 },
                    { 36, 1, 1.0 },
                    { 36, 2, 0.0 },
                    { 36, 3, 0.0 },
                    { 36, 4, 9.0 },
                    { 36, 5, 1.0 },
                    { 36, 6, 0.0 },
                    { 36, 7, 0.0 },
                    { 36, 8, 1.0 },
                    { 36, 9, 0.0 },
                    { 36, 10, 0.0 },
                    { 36, 11, 1.0 },
                    { 37, 1, 0.0 },
                    { 37, 2, 0.0 },
                    { 37, 3, 0.0 },
                    { 37, 4, 9.0 },
                    { 37, 5, 0.0 },
                    { 37, 6, 0.0 },
                    { 37, 7, 0.0 },
                    { 37, 8, 3.0 },
                    { 37, 9, 0.0 },
                    { 37, 10, 0.0 },
                    { 37, 11, 0.0 },
                    { 38, 1, 0.0 },
                    { 38, 2, 1.0 },
                    { 38, 3, 0.0 },
                    { 38, 4, 9.0 },
                    { 38, 5, 1.0 },
                    { 38, 6, 1.0 },
                    { 38, 7, 0.0 },
                    { 38, 8, 0.0 },
                    { 38, 9, 1.0 },
                    { 38, 10, 0.0 },
                    { 38, 11, 0.0 },
                    { 39, 1, 0.0 },
                    { 39, 2, 0.0 },
                    { 39, 3, 4.0 },
                    { 39, 4, 8.0 },
                    { 39, 5, 0.0 },
                    { 39, 6, 0.0 },
                    { 39, 7, 0.0 },
                    { 39, 8, 0.0 },
                    { 39, 9, 0.0 },
                    { 39, 10, 0.0 },
                    { 39, 11, 0.0 },
                    { 40, 1, 0.0 },
                    { 40, 2, 0.0 },
                    { 40, 3, 5.0 },
                    { 40, 4, 8.0 },
                    { 40, 5, 0.0 },
                    { 40, 6, 0.0 },
                    { 40, 7, 0.0 },
                    { 40, 8, 0.0 },
                    { 40, 9, 0.0 },
                    { 40, 10, 0.0 },
                    { 40, 11, 0.0 },
                    { 41, 1, 3.0 },
                    { 41, 2, 1.0 },
                    { 41, 3, 1.0 },
                    { 41, 4, 6.0 },
                    { 41, 5, 3.0 },
                    { 41, 6, 2.0 },
                    { 41, 7, 2.0 },
                    { 41, 8, 3.0 },
                    { 41, 9, 3.0 },
                    { 41, 10, 2.0 },
                    { 41, 11, 0.0 },
                    { 42, 1, 2.0 },
                    { 42, 2, 2.0 },
                    { 42, 3, 0.0 },
                    { 42, 4, 6.0 },
                    { 42, 5, 5.0 },
                    { 42, 6, 0.0 },
                    { 42, 7, 0.0 },
                    { 42, 8, 3.0 },
                    { 42, 9, 2.0 },
                    { 42, 10, 0.0 },
                    { 42, 11, 0.0 },
                    { 43, 1, 0.0 },
                    { 43, 2, 0.0 },
                    { 43, 3, 0.0 },
                    { 43, 4, 10.0 },
                    { 43, 5, 0.0 },
                    { 43, 6, 0.0 },
                    { 43, 7, 0.0 },
                    { 43, 8, 0.0 },
                    { 43, 9, 0.0 },
                    { 43, 10, 0.0 },
                    { 43, 11, 0.0 },
                    { 44, 1, 1.0 },
                    { 44, 2, 0.0 },
                    { 44, 3, 0.0 },
                    { 44, 4, 0.0 },
                    { 44, 5, 10.0 },
                    { 44, 6, 0.0 },
                    { 44, 7, 0.0 },
                    { 44, 8, 0.0 },
                    { 44, 9, 1.0 },
                    { 44, 10, 0.0 },
                    { 44, 11, 3.0 },
                    { 45, 1, 1.0 },
                    { 45, 2, 0.0 },
                    { 45, 3, 0.0 },
                    { 45, 4, 0.0 },
                    { 45, 5, 8.0 },
                    { 45, 6, 0.0 },
                    { 45, 7, 0.0 },
                    { 45, 8, 0.0 },
                    { 45, 9, 0.0 },
                    { 45, 10, 0.0 },
                    { 45, 11, 4.0 },
                    { 46, 1, 0.0 },
                    { 46, 2, 0.0 },
                    { 46, 3, 0.0 },
                    { 46, 4, 0.0 },
                    { 46, 5, 8.0 },
                    { 46, 6, 0.0 },
                    { 46, 7, 0.0 },
                    { 46, 8, 1.0 },
                    { 46, 9, 2.0 },
                    { 46, 10, 0.0 },
                    { 46, 11, 5.0 },
                    { 47, 1, 0.0 },
                    { 47, 2, 0.0 },
                    { 47, 3, 0.0 },
                    { 47, 4, 0.0 },
                    { 47, 5, 8.0 },
                    { 47, 6, 0.0 },
                    { 47, 7, 0.0 },
                    { 47, 8, 1.0 },
                    { 47, 9, 4.0 },
                    { 47, 10, 0.0 },
                    { 47, 11, 1.0 },
                    { 48, 1, 0.0 },
                    { 48, 2, 0.0 },
                    { 48, 3, 0.0 },
                    { 48, 4, 0.0 },
                    { 48, 5, 8.0 },
                    { 48, 6, 0.0 },
                    { 48, 7, 0.0 },
                    { 48, 8, 0.0 },
                    { 48, 9, 1.0 },
                    { 48, 10, 0.0 },
                    { 48, 11, 3.0 },
                    { 49, 1, 0.0 },
                    { 49, 2, 0.0 },
                    { 49, 3, 0.0 },
                    { 49, 4, 1.0 },
                    { 49, 5, 6.0 },
                    { 49, 6, 0.0 },
                    { 49, 7, 0.0 },
                    { 49, 8, 1.0 },
                    { 49, 9, 6.0 },
                    { 49, 10, 0.0 },
                    { 49, 11, 0.0 },
                    { 50, 1, 0.0 },
                    { 50, 2, 0.0 },
                    { 50, 3, 0.0 },
                    { 50, 4, 0.0 },
                    { 50, 5, 9.0 },
                    { 50, 6, 0.0 },
                    { 50, 7, 0.0 },
                    { 50, 8, 0.0 },
                    { 50, 9, 1.0 },
                    { 50, 10, 0.0 },
                    { 50, 11, 1.0 },
                    { 51, 1, 2.0 },
                    { 51, 2, 0.0 },
                    { 51, 3, 0.0 },
                    { 51, 4, 0.0 },
                    { 51, 5, 8.0 },
                    { 51, 6, 0.0 },
                    { 51, 7, 0.0 },
                    { 51, 8, 1.0 },
                    { 51, 9, 0.0 },
                    { 51, 10, 1.0 },
                    { 51, 11, 5.0 },
                    { 52, 1, 3.0 },
                    { 52, 2, 0.0 },
                    { 52, 3, 0.0 },
                    { 52, 4, 0.0 },
                    { 52, 5, 8.0 },
                    { 52, 6, 0.0 },
                    { 52, 7, 0.0 },
                    { 52, 8, 4.0 },
                    { 52, 9, 0.0 },
                    { 52, 10, 0.0 },
                    { 52, 11, 0.0 },
                    { 53, 1, 0.0 },
                    { 53, 2, 0.0 },
                    { 53, 3, 0.0 },
                    { 53, 4, 0.0 },
                    { 53, 5, 0.0 },
                    { 53, 6, 10.0 },
                    { 53, 7, 0.0 },
                    { 53, 8, 0.0 },
                    { 53, 9, 0.0 },
                    { 53, 10, 0.0 },
                    { 53, 11, 0.0 },
                    { 54, 1, 0.0 },
                    { 54, 2, 0.0 },
                    { 54, 3, 0.0 },
                    { 54, 4, 0.0 },
                    { 54, 5, 1.0 },
                    { 54, 6, 9.0 },
                    { 54, 7, 0.0 },
                    { 54, 8, 0.0 },
                    { 54, 9, 0.0 },
                    { 54, 10, 0.0 },
                    { 54, 11, 3.0 },
                    { 55, 1, 0.0 },
                    { 55, 2, 0.0 },
                    { 55, 3, 0.0 },
                    { 55, 4, 0.0 },
                    { 55, 5, 0.0 },
                    { 55, 6, 9.0 },
                    { 55, 7, 0.0 },
                    { 55, 8, 0.0 },
                    { 55, 9, 0.0 },
                    { 55, 10, 0.0 },
                    { 55, 11, 1.0 },
                    { 56, 1, 0.0 },
                    { 56, 2, 0.0 },
                    { 56, 3, 0.0 },
                    { 56, 4, 0.0 },
                    { 56, 5, 0.0 },
                    { 56, 6, 9.0 },
                    { 56, 7, 0.0 },
                    { 56, 8, 0.0 },
                    { 56, 9, 0.0 },
                    { 56, 10, 0.0 },
                    { 56, 11, 2.0 },
                    { 57, 1, 0.0 },
                    { 57, 2, 0.0 },
                    { 57, 3, 0.0 },
                    { 57, 4, 0.0 },
                    { 57, 5, 0.0 },
                    { 57, 6, 7.0 },
                    { 57, 7, 0.0 },
                    { 57, 8, 0.0 },
                    { 57, 9, 0.0 },
                    { 57, 10, 0.0 },
                    { 57, 11, 6.0 },
                    { 58, 1, 1.0 },
                    { 58, 2, 0.0 },
                    { 58, 3, 0.0 },
                    { 58, 4, 0.0 },
                    { 58, 5, 2.0 },
                    { 58, 6, 8.0 },
                    { 58, 7, 2.0 },
                    { 58, 8, 0.0 },
                    { 58, 9, 0.0 },
                    { 58, 10, 0.0 },
                    { 58, 11, 0.0 },
                    { 59, 1, 0.0 },
                    { 59, 2, 0.0 },
                    { 59, 3, 0.0 },
                    { 59, 4, 1.0 },
                    { 59, 5, 0.0 },
                    { 59, 6, 9.0 },
                    { 59, 7, 0.0 },
                    { 59, 8, 0.0 },
                    { 59, 9, 0.0 },
                    { 59, 10, 0.0 },
                    { 59, 11, 0.0 },
                    { 60, 1, 0.0 },
                    { 60, 2, 0.0 },
                    { 60, 3, 0.0 },
                    { 60, 4, 0.0 },
                    { 60, 5, 0.0 },
                    { 60, 6, 7.0 },
                    { 60, 7, 0.0 },
                    { 60, 8, 0.0 },
                    { 60, 9, 0.0 },
                    { 60, 10, 0.0 },
                    { 60, 11, 4.0 },
                    { 61, 1, 0.0 },
                    { 61, 2, 0.0 },
                    { 61, 3, 0.0 },
                    { 61, 4, 1.0 },
                    { 61, 5, 0.0 },
                    { 61, 6, 6.0 },
                    { 61, 7, 5.0 },
                    { 61, 8, 1.0 },
                    { 61, 9, 0.0 },
                    { 61, 10, 0.0 },
                    { 61, 11, 0.0 },
                    { 62, 1, 0.0 },
                    { 62, 2, 0.0 },
                    { 62, 3, 0.0 },
                    { 62, 4, 0.0 },
                    { 62, 5, 0.0 },
                    { 62, 6, 0.0 },
                    { 62, 7, 10.0 },
                    { 62, 8, 0.0 },
                    { 62, 9, 0.0 },
                    { 62, 10, 0.0 },
                    { 62, 11, 0.0 },
                    { 63, 1, 0.0 },
                    { 63, 2, 0.0 },
                    { 63, 3, 0.0 },
                    { 63, 4, 0.0 },
                    { 63, 5, 2.0 },
                    { 63, 6, 0.0 },
                    { 63, 7, 9.0 },
                    { 63, 8, 0.0 },
                    { 63, 9, 0.0 },
                    { 63, 10, 0.0 },
                    { 63, 11, 3.0 },
                    { 64, 1, 0.0 },
                    { 64, 2, 0.0 },
                    { 64, 3, 0.0 },
                    { 64, 4, 1.0 },
                    { 64, 5, 0.0 },
                    { 64, 6, 0.0 },
                    { 64, 7, 10.0 },
                    { 64, 8, 0.0 },
                    { 64, 9, 0.0 },
                    { 64, 10, 0.0 },
                    { 64, 11, 0.0 },
                    { 65, 1, 0.0 },
                    { 65, 2, 0.0 },
                    { 65, 3, 0.0 },
                    { 65, 4, 0.0 },
                    { 65, 5, 0.0 },
                    { 65, 6, 0.0 },
                    { 65, 7, 7.0 },
                    { 65, 8, 0.0 },
                    { 65, 9, 0.0 },
                    { 65, 10, 0.0 },
                    { 65, 11, 6.0 },
                    { 66, 1, 0.0 },
                    { 66, 2, 0.0 },
                    { 66, 3, 0.0 },
                    { 66, 4, 0.0 },
                    { 66, 5, 0.0 },
                    { 66, 6, 0.0 },
                    { 66, 7, 7.0 },
                    { 66, 8, 0.0 },
                    { 66, 9, 0.0 },
                    { 66, 10, 0.0 },
                    { 66, 11, 5.0 },
                    { 67, 1, 1.0 },
                    { 67, 2, 0.0 },
                    { 67, 3, 1.0 },
                    { 67, 4, 0.0 },
                    { 67, 5, 0.0 },
                    { 67, 6, 0.0 },
                    { 67, 7, 7.0 },
                    { 67, 8, 0.0 },
                    { 67, 9, 0.0 },
                    { 67, 10, 0.0 },
                    { 67, 11, 3.0 },
                    { 68, 1, 0.0 },
                    { 68, 2, 0.0 },
                    { 68, 3, 0.0 },
                    { 68, 4, 0.0 },
                    { 68, 5, 0.0 },
                    { 68, 6, 0.0 },
                    { 68, 7, 10.0 },
                    { 68, 8, 0.0 },
                    { 68, 9, 0.0 },
                    { 68, 10, 0.0 },
                    { 68, 11, 0.0 },
                    { 69, 1, 0.0 },
                    { 69, 2, 0.0 },
                    { 69, 3, 0.0 },
                    { 69, 4, 0.0 },
                    { 69, 5, 0.0 },
                    { 69, 6, 1.0 },
                    { 69, 7, 9.0 },
                    { 69, 8, 0.0 },
                    { 69, 9, 0.0 },
                    { 69, 10, 0.0 },
                    { 69, 11, 0.0 },
                    { 70, 1, 0.0 },
                    { 70, 2, 0.0 },
                    { 70, 3, 0.0 },
                    { 70, 4, 3.0 },
                    { 70, 5, 0.0 },
                    { 70, 6, 1.0 },
                    { 70, 7, 8.0 },
                    { 70, 8, 0.0 },
                    { 70, 9, 1.0 },
                    { 70, 10, 0.0 },
                    { 70, 11, 1.0 },
                    { 71, 1, 0.0 },
                    { 71, 2, 0.0 },
                    { 71, 3, 0.0 },
                    { 71, 4, 0.0 },
                    { 71, 5, 0.0 },
                    { 71, 6, 0.0 },
                    { 71, 7, 0.0 },
                    { 71, 8, 10.0 },
                    { 71, 9, 1.0 },
                    { 71, 10, 0.0 },
                    { 71, 11, 0.0 },
                    { 72, 1, 0.0 },
                    { 72, 2, 0.0 },
                    { 72, 3, 0.0 },
                    { 72, 4, 0.0 },
                    { 72, 5, 0.0 },
                    { 72, 6, 0.0 },
                    { 72, 7, 0.0 },
                    { 72, 8, 9.0 },
                    { 72, 9, 4.0 },
                    { 72, 10, 0.0 },
                    { 72, 11, 0.0 },
                    { 73, 1, 0.0 },
                    { 73, 2, 0.0 },
                    { 73, 3, 0.0 },
                    { 73, 4, 2.0 },
                    { 73, 5, 0.0 },
                    { 73, 6, 0.0 },
                    { 73, 7, 0.0 },
                    { 73, 8, 9.0 },
                    { 73, 9, 0.0 },
                    { 73, 10, 0.0 },
                    { 73, 11, 0.0 },
                    { 74, 1, 0.0 },
                    { 74, 2, 0.0 },
                    { 74, 3, 0.0 },
                    { 74, 4, 1.0 },
                    { 74, 5, 1.0 },
                    { 74, 6, 0.0 },
                    { 74, 7, 0.0 },
                    { 74, 8, 9.0 },
                    { 74, 9, 0.0 },
                    { 74, 10, 0.0 },
                    { 74, 11, 0.0 },
                    { 75, 1, 1.0 },
                    { 75, 2, 0.0 },
                    { 75, 3, 0.0 },
                    { 75, 4, 0.0 },
                    { 75, 5, 0.0 },
                    { 75, 6, 0.0 },
                    { 75, 7, 0.0 },
                    { 75, 8, 8.0 },
                    { 75, 9, 0.0 },
                    { 75, 10, 0.0 },
                    { 75, 11, 3.0 },
                    { 76, 1, 0.0 },
                    { 76, 2, 0.0 },
                    { 76, 3, 0.0 },
                    { 76, 4, 0.0 },
                    { 76, 5, 0.0 },
                    { 76, 6, 0.0 },
                    { 76, 7, 0.0 },
                    { 76, 8, 7.0 },
                    { 76, 9, 0.0 },
                    { 76, 10, 0.0 },
                    { 76, 11, 6.0 },
                    { 77, 1, 0.0 },
                    { 77, 2, 0.0 },
                    { 77, 3, 0.0 },
                    { 77, 4, 2.0 },
                    { 77, 5, 0.0 },
                    { 77, 6, 0.0 },
                    { 77, 7, 0.0 },
                    { 77, 8, 9.0 },
                    { 77, 9, 0.0 },
                    { 77, 10, 0.0 },
                    { 77, 11, 0.0 },
                    { 78, 1, 0.0 },
                    { 78, 2, 0.0 },
                    { 78, 3, 0.0 },
                    { 78, 4, 1.0 },
                    { 78, 5, 0.0 },
                    { 78, 6, 1.0 },
                    { 78, 7, 1.0 },
                    { 78, 8, 9.0 },
                    { 78, 9, 0.0 },
                    { 78, 10, 0.0 },
                    { 78, 11, 0.0 },
                    { 79, 1, 0.0 },
                    { 79, 2, 0.0 },
                    { 79, 3, 0.0 },
                    { 79, 4, 0.0 },
                    { 79, 5, 0.0 },
                    { 79, 6, 0.0 },
                    { 79, 7, 0.0 },
                    { 79, 8, 7.0 },
                    { 79, 9, 0.0 },
                    { 79, 10, 0.0 },
                    { 79, 11, 5.0 },
                    { 80, 1, 0.0 },
                    { 80, 2, 0.0 },
                    { 80, 3, 0.0 },
                    { 80, 4, 1.0 },
                    { 80, 5, 1.0 },
                    { 80, 6, 0.0 },
                    { 80, 7, 0.0 },
                    { 80, 8, 1.0 },
                    { 80, 9, 10.0 },
                    { 80, 10, 0.0 },
                    { 80, 11, 0.0 },
                    { 81, 1, 0.0 },
                    { 81, 2, 0.0 },
                    { 81, 3, 0.0 },
                    { 81, 4, 0.0 },
                    { 81, 5, 1.0 },
                    { 81, 6, 0.0 },
                    { 81, 7, 0.0 },
                    { 81, 8, 0.0 },
                    { 81, 9, 9.0 },
                    { 81, 10, 0.0 },
                    { 81, 11, 3.0 },
                    { 82, 1, 0.0 },
                    { 82, 2, 0.0 },
                    { 82, 3, 0.0 },
                    { 82, 4, 0.0 },
                    { 82, 5, 1.0 },
                    { 82, 6, 0.0 },
                    { 82, 7, 0.0 },
                    { 82, 8, 0.0 },
                    { 82, 9, 10.0 },
                    { 82, 10, 0.0 },
                    { 82, 11, 0.0 },
                    { 83, 1, 0.0 },
                    { 83, 2, 0.0 },
                    { 83, 3, 0.0 },
                    { 83, 4, 2.0 },
                    { 83, 5, 0.0 },
                    { 83, 6, 0.0 },
                    { 83, 7, 0.0 },
                    { 83, 8, 0.0 },
                    { 83, 9, 10.0 },
                    { 83, 10, 0.0 },
                    { 83, 11, 0.0 },
                    { 84, 1, 0.0 },
                    { 84, 2, 0.0 },
                    { 84, 3, 0.0 },
                    { 84, 4, 1.0 },
                    { 84, 5, 0.0 },
                    { 84, 6, 0.0 },
                    { 84, 7, 0.0 },
                    { 84, 8, 0.0 },
                    { 84, 9, 10.0 },
                    { 84, 10, 0.0 },
                    { 84, 11, 0.0 },
                    { 85, 1, 0.0 },
                    { 85, 2, 0.0 },
                    { 85, 3, 0.0 },
                    { 85, 4, 2.0 },
                    { 85, 5, 0.0 },
                    { 85, 6, 0.0 },
                    { 85, 7, 0.0 },
                    { 85, 8, 0.0 },
                    { 85, 9, 9.0 },
                    { 85, 10, 0.0 },
                    { 85, 11, 0.0 },
                    { 86, 1, 0.0 },
                    { 86, 2, 0.0 },
                    { 86, 3, 0.0 },
                    { 86, 4, 0.0 },
                    { 86, 5, 1.0 },
                    { 86, 6, 0.0 },
                    { 86, 7, 0.0 },
                    { 86, 8, 1.0 },
                    { 86, 9, 8.0 },
                    { 86, 10, 0.0 },
                    { 86, 11, 5.0 },
                    { 87, 1, 0.0 },
                    { 87, 2, 0.0 },
                    { 87, 3, 0.0 },
                    { 87, 4, 1.0 },
                    { 87, 5, 1.0 },
                    { 87, 6, 0.0 },
                    { 87, 7, 0.0 },
                    { 87, 8, 0.0 },
                    { 87, 9, 10.0 },
                    { 87, 10, 0.0 },
                    { 87, 11, 0.0 },
                    { 88, 1, 1.0 },
                    { 88, 2, 1.0 },
                    { 88, 3, 0.0 },
                    { 88, 4, 3.0 },
                    { 88, 5, 1.0 },
                    { 88, 6, 1.0 },
                    { 88, 7, 1.0 },
                    { 88, 8, 2.0 },
                    { 88, 9, 8.0 },
                    { 88, 10, 0.0 },
                    { 88, 11, 0.0 },
                    { 89, 1, 3.0 },
                    { 89, 2, 0.0 },
                    { 89, 3, 0.0 },
                    { 89, 4, 0.0 },
                    { 89, 5, 0.0 },
                    { 89, 6, 0.0 },
                    { 89, 7, 0.0 },
                    { 89, 8, 0.0 },
                    { 89, 9, 0.0 },
                    { 89, 10, 10.0 },
                    { 89, 11, 0.0 },
                    { 90, 1, 1.0 },
                    { 90, 2, 0.0 },
                    { 90, 3, 0.0 },
                    { 90, 4, 0.0 },
                    { 90, 5, 0.0 },
                    { 90, 6, 0.0 },
                    { 90, 7, 0.0 },
                    { 90, 8, 0.0 },
                    { 90, 9, 0.0 },
                    { 90, 10, 10.0 },
                    { 90, 11, 0.0 },
                    { 91, 1, 1.0 },
                    { 91, 2, 0.0 },
                    { 91, 3, 0.0 },
                    { 91, 4, 0.0 },
                    { 91, 5, 0.0 },
                    { 91, 6, 0.0 },
                    { 91, 7, 0.0 },
                    { 91, 8, 0.0 },
                    { 91, 9, 0.0 },
                    { 91, 10, 9.0 },
                    { 91, 11, 0.0 },
                    { 92, 1, 0.0 },
                    { 92, 2, 0.0 },
                    { 92, 3, 0.0 },
                    { 92, 4, 1.0 },
                    { 92, 5, 0.0 },
                    { 92, 6, 0.0 },
                    { 92, 7, 0.0 },
                    { 92, 8, 0.0 },
                    { 92, 9, 0.0 },
                    { 92, 10, 9.0 },
                    { 92, 11, 0.0 },
                    { 93, 1, 1.0 },
                    { 93, 2, 0.0 },
                    { 93, 3, 0.0 },
                    { 93, 4, 0.0 },
                    { 93, 5, 1.0 },
                    { 93, 6, 0.0 },
                    { 93, 7, 0.0 },
                    { 93, 8, 0.0 },
                    { 93, 9, 0.0 },
                    { 93, 10, 7.0 },
                    { 93, 11, 6.0 },
                    { 94, 1, 1.0 },
                    { 94, 2, 0.0 },
                    { 94, 3, 0.0 },
                    { 94, 4, 0.0 },
                    { 94, 5, 0.0 },
                    { 94, 6, 0.0 },
                    { 94, 7, 0.0 },
                    { 94, 8, 0.0 },
                    { 94, 9, 0.0 },
                    { 94, 10, 9.0 },
                    { 94, 11, 0.0 },
                    { 95, 1, 4.0 },
                    { 95, 2, 0.0 },
                    { 95, 3, 0.0 },
                    { 95, 4, 0.0 },
                    { 95, 5, 0.0 },
                    { 95, 6, 0.0 },
                    { 95, 7, 0.0 },
                    { 95, 8, 0.0 },
                    { 95, 9, 0.0 },
                    { 95, 10, 8.0 },
                    { 95, 11, 0.0 },
                    { 96, 1, 3.0 },
                    { 96, 2, 0.0 },
                    { 96, 3, 1.0 },
                    { 96, 4, 0.0 },
                    { 96, 5, 3.0 },
                    { 96, 6, 1.0 },
                    { 96, 7, 1.0 },
                    { 96, 8, 0.0 },
                    { 96, 9, 0.0 },
                    { 96, 10, 1.0 },
                    { 96, 11, 9.0 },
                    { 97, 1, 2.0 },
                    { 97, 2, 0.0 },
                    { 97, 3, 0.0 },
                    { 97, 4, 1.0 },
                    { 97, 5, 1.0 },
                    { 97, 6, 0.0 },
                    { 97, 7, 1.0 },
                    { 97, 8, 0.0 },
                    { 97, 9, 2.0 },
                    { 97, 10, 0.0 },
                    { 97, 11, 10.0 },
                    { 98, 1, 2.0 },
                    { 98, 2, 0.0 },
                    { 98, 3, 0.0 },
                    { 98, 4, 0.0 },
                    { 98, 5, 3.0 },
                    { 98, 6, 1.0 },
                    { 98, 7, 1.0 },
                    { 98, 8, 0.0 },
                    { 98, 9, 0.0 },
                    { 98, 10, 1.0 },
                    { 98, 11, 9.0 },
                    { 99, 1, 3.0 },
                    { 99, 2, 0.0 },
                    { 99, 3, 0.0 },
                    { 99, 4, 0.0 },
                    { 99, 5, 3.0 },
                    { 99, 6, 0.0 },
                    { 99, 7, 0.0 },
                    { 99, 8, 0.0 },
                    { 99, 9, 0.0 },
                    { 99, 10, 4.0 },
                    { 99, 11, 9.0 },
                    { 100, 1, 0.0 },
                    { 100, 2, 0.0 },
                    { 100, 3, 2.0 },
                    { 100, 4, 0.0 },
                    { 100, 5, 2.0 },
                    { 100, 6, 0.0 },
                    { 100, 7, 0.0 },
                    { 100, 8, 1.0 },
                    { 100, 9, 2.0 },
                    { 100, 10, 0.0 },
                    { 100, 11, 8.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_UserId",
                table: "CartItem",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWeight_IdProductType",
                table: "ProductWeight",
                column: "IdProductType");

            migrationBuilder.CreateIndex(
                name: "IX_UserWeight_IdProductType",
                table: "UserWeight",
                column: "IdProductType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "ProductWeight");

            migrationBuilder.DropTable(
                name: "UserWeight");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "IdProductType",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "IdProductType",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "IdProductType",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "IdProductType",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "IdProductType",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "IdProductType",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "IdProductType",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8152a3a2-fa24-48ef-b41d-f924c569049b", "AQAAAAIAAYagAAAAEGGNmcRp7f1rf0nvhAw7gFi/c9FDy64RoBg+ccS9rwkDmxodu4VtYX+Nc+zfJL7n3g==", "f46b4309-944b-4310-8429-2cc9352cb517" });
        }
    }
}
