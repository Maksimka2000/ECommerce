using E_commerce.Entities;

namespace E_commerce.Context;

// Seed catalog: 11 categories (10 + Other) and 100 products, each with a vector of 11 weights (0-10).
// Vector order is always by category id:
// [Gym, Swimming, Tennis, Other, Running, Football, Basketball, Cycling, Hiking, Yoga, Clothing]
public static class SeedData
{
    public static readonly ProductType[] ProductTypes =
    {
        new ProductType { IdProductType = 1, TypeName = "Gym Equipment" },
        new ProductType { IdProductType = 2, TypeName = "Swimming" },
        new ProductType { IdProductType = 3, TypeName = "Tennis" },
        new ProductType { IdProductType = 4, TypeName = "Other" },
        new ProductType { IdProductType = 5, TypeName = "Running" },
        new ProductType { IdProductType = 6, TypeName = "Football" },
        new ProductType { IdProductType = 7, TypeName = "Basketball" },
        new ProductType { IdProductType = 8, TypeName = "Cycling" },
        new ProductType { IdProductType = 9, TypeName = "Hiking" },
        new ProductType { IdProductType = 10, TypeName = "Yoga" },
        new ProductType { IdProductType = 11, TypeName = "Clothing" }
    };

    private const string NoPhoto = "no-photo.svg";

    private static readonly List<(Product Product, double[] Vector)> Items = new()
    {
        //                                                                                                         Gym Swm Ten Oth Run Fbl Bsk Cyc Hik Yog Clo
        // Gym Equipment
        P(1, "Bar", " Heavy-duty barbell for strength training.", 120.50M, 1, "bar.jpg",                         10, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0),
        P(2, "Rope", "Durable skipping rope for cardio workouts.", 15.99M, 1, "rope.jpg",                         8, 1, 3, 1, 4, 2, 2, 0, 0, 0, 0),
        P(9, "Dumbbell Set 20kg", "Pair of adjustable dumbbells for home workouts.", 89.00M, 1, "bar.jpg",       10, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0),
        P(10, "Kettlebell 16kg", "Cast iron kettlebell for swings and squats.", 49.00M, 1, "p-10.jpg",               9, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0),
        P(11, "Adjustable Bench", "Foldable bench with 6 incline positions.", 159.00M, 1, "p-11.jpg",               9, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0),
        P(12, "Pull-up Bar", "Doorway pull-up bar, no screws needed.", 29.99M, 1, "p-12.jpg",                       8, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0),
        P(13, "Resistance Bands Set", "Five bands with different resistance levels.", 24.50M, 1, "p-13.jpg",        7, 0, 1, 0, 2, 1, 1, 0, 0, 4, 0),
        P(14, "Weight Plates 2x10kg", "Rubber coated plates for a standard bar.", 69.00M, 1, "bar.jpg",          10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
        P(15, "Lifting Gloves", "Padded gloves with wrist support.", 19.99M, 1, "p-15.jpg",                         7, 0, 0, 0, 0, 0, 0, 1, 0, 0, 4),
        P(16, "Gym Bag", "Spacious bag with a shoe compartment.", 39.00M, 1, "p-16.jpg",                            6, 2, 2, 3, 2, 2, 2, 0, 1, 1, 2),
        P(17, "Foam Roller", "Firm roller for muscle recovery.", 22.00M, 1, "p-17.jpg",                             6, 0, 0, 0, 3, 1, 1, 1, 0, 4, 0),

        // Swimming
        P(3, "Dive Mask", "High-quality dive mask for underwater adventures.", 45.00M, 2, "divemask.jpg",         0, 10, 0, 2, 0, 0, 0, 0, 0, 0, 0),
        P(4, "Swimming Equipment", "Complete set of swimming gear for beginners.", 75.00M, 2, "swim.jpg",         1, 9, 0, 1, 0, 0, 0, 0, 0, 0, 2),
        P(5, "Mask", "Comfortable mask for professional swimmers.", 30.00M, 2, "mask.jpg",                        0, 9, 0, 1, 0, 0, 0, 0, 0, 0, 0),
        P(18, "Swimming Shorts", "Quick-dry shorts for the pool and the beach.", 25.00M, 2, "p-18.jpg",              0, 8, 0, 1, 0, 0, 0, 0, 0, 0, 4),
        P(19, "Swim Goggles", "Anti-fog goggles with UV protection.", 14.99M, 2, "p-19.jpg",                         0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0),
        P(20, "Swim Cap", "Silicone cap that keeps hair dry.", 9.99M, 2, "p-20.jpg",                                 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 2),
        P(21, "Kickboard", "Foam board for leg training.", 12.50M, 2, "p-21.jpg",                                    1, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0),
        P(22, "Swimsuit", "Chlorine resistant one-piece swimsuit.", 39.00M, 2, "p-22.jpg",                           0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 5),
        P(23, "Fins", "Short training fins for speed.", 34.00M, 2, "p-23.jpg",                                       0, 9, 0, 1, 0, 0, 0, 0, 0, 0, 0),
        P(24, "Full Face Snorkel", "Full face snorkel mask with a dry top.", 49.00M, 2, "snorkel.jpg",           0, 9, 0, 3, 0, 0, 0, 0, 1, 0, 0),
        P(25, "Pull Buoy", "Foam buoy for arm training.", 11.00M, 2, "p-25.jpg",                                     1, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0),
        P(26, "Microfiber Towel", "Light towel that dries fast.", 17.00M, 2, "p-26.jpg",                             1, 6, 0, 2, 1, 0, 0, 0, 2, 2, 0),

        // Tennis
        P(6, "Big Racket", "Professional tennis racket for power hitters.", 200.00M, 3, "teniss.jpg",             1, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0),
        P(7, "Small Racket", "Lightweight tennis racket for juniors.", 150.00M, 3, "racket.jpg",                  1, 0, 9, 1, 0, 0, 0, 0, 0, 0, 0),
        P(27, "Tennis Balls (3 pack)", "Pressurized balls for all court types.", 8.99M, 3, "teniss.jpg",          0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0),
        P(28, "Tennis Shoes", "Court shoes with lateral support.", 99.00M, 3, "p-28.jpg",                            0, 0, 8, 0, 2, 0, 0, 0, 0, 0, 4),
        P(29, "Racket Bag", "Bag for up to 6 rackets.", 59.00M, 3, "p-29.jpg",                                       0, 0, 8, 2, 0, 0, 0, 0, 0, 0, 1),
        P(30, "Overgrip Pack", "Pack of 12 tacky overgrips.", 12.00M, 3, "racket.jpg",                            0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0),
        P(31, "Tennis Skirt", "Light skirt with built-in shorts.", 35.00M, 3, "p-31.jpg",                            0, 0, 7, 0, 1, 0, 0, 0, 0, 0, 5),
        P(32, "Wristbands", "Cotton wristbands, pair.", 6.50M, 3, "p-32.jpg",                                        1, 0, 6, 0, 1, 0, 2, 0, 0, 0, 3),
        P(33, "Tennis Cap", "Breathable cap with a long visor.", 18.00M, 3, "p-33.jpg",                              0, 0, 6, 0, 2, 0, 0, 0, 1, 0, 4),
        P(34, "Ball Machine", "Portable machine for solo training.", 499.00M, 3, "p-34.jpg",                         0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0),
        P(35, "Tennis Net", "Regulation size net with steel cable.", 129.00M, 3, "p-35.jpg",                         0, 0, 10, 1, 0, 0, 0, 0, 0, 0, 0),

        // Other
        P(8, "Skateboard", "Stylish skateboard for street and park use.", 100.00M, 4, "skate.jpg",                1, 0, 0, 10, 0, 0, 0, 2, 0, 0, 1),
        P(36, "Roller Skates", "Classic quad skates with soft wheels.", 89.00M, 4, "roll.jpg",                    1, 0, 0, 9, 1, 0, 0, 1, 0, 0, 1),
        P(37, "Kick Scooter", "Foldable aluminium scooter.", 79.00M, 4, "skate.jpg",                              0, 0, 0, 9, 0, 0, 0, 3, 0, 0, 0),
        P(38, "Frisbee", "Ultimate disc, 175g.", 14.00M, 4, "p-38.jpg",                                              0, 1, 0, 9, 1, 1, 0, 0, 1, 0, 0),
        P(39, "Badminton Set", "Two rackets, net and shuttles.", 45.00M, 4, "p-39.jpg",                              0, 0, 4, 8, 0, 0, 0, 0, 0, 0, 0),
        P(40, "Table Tennis Paddles", "Two paddles and three balls.", 29.00M, 4, "p-40.jpg",                         0, 0, 5, 8, 0, 0, 0, 0, 0, 0, 0),
        P(41, "Water Bottle", "Insulated steel bottle, 750 ml.", 19.00M, 4, "p-41.jpg",                              3, 1, 1, 6, 3, 2, 2, 3, 3, 2, 0),
        P(42, "Sports Watch", "GPS watch with heart rate tracking.", 199.00M, 4, "p-42.jpg",                         2, 2, 0, 6, 5, 0, 0, 3, 2, 0, 0),
        P(43, "Darts Set", "Steel tip darts with a board.", 39.00M, 4, "p-43.jpg",                                   0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0),

        // Running
        P(44, "Running Shoes", "Cushioned shoes for road running.", 119.00M, 5, "p-44.jpg",                          1, 0, 0, 0, 10, 0, 0, 0, 1, 0, 3),
        P(45, "Running Shorts", "Light shorts with an inner pocket.", 29.00M, 5, "p-45.jpg",                         1, 0, 0, 0, 8, 0, 0, 0, 0, 0, 4),
        P(46, "Running Jacket", "Windproof jacket with reflective details.", 79.00M, 5, "p-46.jpg",                  0, 0, 0, 0, 8, 0, 0, 1, 2, 0, 5),
        P(47, "Hydration Vest", "Vest with two soft flasks.", 69.00M, 5, "p-47.jpg",                                 0, 0, 0, 0, 8, 0, 0, 1, 4, 0, 1),
        P(48, "Running Socks", "Anti-blister socks, 3 pairs.", 15.00M, 5, "p-48.jpg",                                0, 0, 0, 0, 8, 0, 0, 0, 1, 0, 3),
        P(49, "Headlamp", "Rechargeable headlamp, 400 lumen.", 35.00M, 5, NoPhoto,                                0, 0, 0, 1, 6, 0, 0, 1, 6, 0, 0),
        P(50, "Running Belt", "Belt for phone and keys.", 17.00M, 5, "p-50.jpg",                                     0, 0, 0, 0, 9, 0, 0, 0, 1, 0, 1),
        P(51, "Compression Tights", "Tights that support the muscles.", 49.00M, 5, "p-51.jpg",                       2, 0, 0, 0, 8, 0, 0, 1, 0, 1, 5),
        P(52, "Heart Rate Monitor", "Chest strap with Bluetooth.", 59.00M, 5, "p-52.jpg",                            3, 0, 0, 0, 8, 0, 0, 4, 0, 0, 0),

        // Football
        P(53, "Football Ball", "Size 5 match ball.", 29.00M, 6, "p-53.jpg",                                          0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0),
        P(54, "Football Boots", "Boots for firm ground.", 109.00M, 6, "p-54.jpg",                                    0, 0, 0, 0, 1, 9, 0, 0, 0, 0, 3),
        P(55, "Shin Guards", "Light guards with ankle protection.", 19.00M, 6, "p-55.jpg",                           0, 0, 0, 0, 0, 9, 0, 0, 0, 0, 1),
        P(56, "Goalkeeper Gloves", "Gloves with latex palm.", 39.00M, 6, "p-56.jpg",                                 0, 0, 0, 0, 0, 9, 0, 0, 0, 0, 2),
        P(57, "Football Jersey", "Breathable team jersey.", 45.00M, 6, "p-57.jpg",                                   0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 6),
        P(58, "Training Cones", "Set of 20 cones for drills.", 12.00M, 6, "p-58.jpg",                                1, 0, 0, 0, 2, 8, 2, 0, 0, 0, 0),
        P(59, "Mini Goal", "Pop-up goal for the garden.", 59.00M, 6, "p-59.jpg",                                     0, 0, 0, 1, 0, 9, 0, 0, 0, 0, 0),
        P(60, "Football Socks", "Long socks, one size.", 9.00M, 6, "p-60.jpg",                                       0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 4),
        P(61, "Ball Pump", "Hand pump with needle.", 8.00M, 6, "p-61.jpg",                                           0, 0, 0, 1, 0, 6, 5, 1, 0, 0, 0),

        // Basketball
        P(62, "Basketball Ball", "Size 7 indoor/outdoor ball.", 35.00M, 7, "p-62.jpg",                               0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0),
        P(63, "Basketball Shoes", "High top shoes with ankle support.", 129.00M, 7, "p-63.jpg",                      0, 0, 0, 0, 2, 0, 9, 0, 0, 0, 3),
        P(64, "Basketball Hoop", "Wall mounted hoop with net.", 149.00M, 7, "p-64.jpg",                              0, 0, 0, 1, 0, 0, 10, 0, 0, 0, 0),
        P(65, "Basketball Jersey", "Sleeveless mesh jersey.", 39.00M, 7, "p-65.jpg",                                 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 6),
        P(66, "Basketball Shorts", "Long shorts with side pockets.", 32.00M, 7, "p-66.jpg",                          0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 5),
        P(67, "Arm Sleeve", "Compression sleeve, one arm.", 12.00M, 7, "p-67.jpg",                                   1, 0, 1, 0, 0, 0, 7, 0, 0, 0, 3),
        P(68, "Basketball Net", "Heavy duty replacement net.", 10.00M, 7, "p-68.jpg",                                0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0),
        P(69, "Dribble Goggles", "Goggles that block the view of the ball.", 15.00M, 7, "p-69.jpg",                  0, 0, 0, 0, 0, 1, 9, 0, 0, 0, 0),
        P(70, "Basketball Bag", "Backpack with a ball net.", 45.00M, 7, "p-70.jpg",                                  0, 0, 0, 3, 0, 1, 8, 0, 1, 0, 1),

        // Cycling
        P(71, "Road Bike", "Aluminium road bike, 18 speeds.", 899.00M, 8, "p-71.jpg",                                0, 0, 0, 0, 0, 0, 0, 10, 1, 0, 0),
        P(72, "Mountain Bike", "Hardtail MTB with front suspension.", 749.00M, 8, "p-72.jpg",                        0, 0, 0, 0, 0, 0, 0, 9, 4, 0, 0),
        P(73, "Cycling Helmet", "Vented helmet with a rear light.", 69.00M, 8, "p-73.jpg",                           0, 0, 0, 2, 0, 0, 0, 9, 0, 0, 0),
        P(74, "Bike Lights", "Front and rear USB lights.", 25.00M, 8, "p-74.jpg",                                    0, 0, 0, 1, 1, 0, 0, 9, 0, 0, 0),
        P(75, "Cycling Gloves", "Half finger padded gloves.", 22.00M, 8, "p-75.jpg",                                 1, 0, 0, 0, 0, 0, 0, 8, 0, 0, 3),
        P(76, "Cycling Jersey", "Jersey with three back pockets.", 59.00M, 8, "p-76.jpg",                            0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 6),
        P(77, "Bike Lock", "Hardened steel U-lock.", 35.00M, 8, "p-77.jpg",                                          0, 0, 0, 2, 0, 0, 0, 9, 0, 0, 0),
        P(78, "Bike Pump", "Floor pump with gauge.", 29.00M, 8, "p-78.jpg",                                          0, 0, 0, 1, 0, 1, 1, 9, 0, 0, 0),
        P(79, "Cycling Shorts", "Bib shorts with a padded insert.", 65.00M, 8, "p-79.jpg",                           0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 5),

        // Hiking
        P(80, "Hiking Backpack", "40 L backpack with a rain cover.", 89.00M, 9, "p-80.jpg",                          0, 0, 0, 1, 1, 0, 0, 1, 10, 0, 0),
        P(81, "Hiking Boots", "Waterproof boots with a grippy sole.", 139.00M, 9, "p-81.jpg",                        0, 0, 0, 0, 1, 0, 0, 0, 9, 0, 3),
        P(82, "Trekking Poles", "Pair of foldable aluminium poles.", 49.00M, 9, "p-82.jpg",                          0, 0, 0, 0, 1, 0, 0, 0, 10, 0, 0),
        P(83, "Tent 2P", "Two person tent, 2.1 kg.", 199.00M, 9, "p-83.jpg",                                         0, 0, 0, 2, 0, 0, 0, 0, 10, 0, 0),
        P(84, "Sleeping Bag", "Three season sleeping bag.", 79.00M, 9, "p-84.jpg",                                   0, 0, 0, 1, 0, 0, 0, 0, 10, 0, 0),
        P(85, "Camping Stove", "Compact gas stove.", 39.00M, 9, "p-85.jpg",                                          0, 0, 0, 2, 0, 0, 0, 0, 9, 0, 0),
        P(86, "Hiking Jacket", "Waterproof shell jacket.", 149.00M, 9, "p-86.jpg",                                   0, 0, 0, 0, 1, 0, 0, 1, 8, 0, 5),
        P(87, "Compass", "Baseplate compass with a mirror.", 19.00M, 9, "p-87.jpg",                                  0, 0, 0, 1, 1, 0, 0, 0, 10, 0, 0),
        P(88, "First Aid Kit", "Light kit for the trail.", 25.00M, 9, "p-88.jpg",                                    1, 1, 0, 3, 1, 1, 1, 2, 8, 0, 0),

        // Yoga
        P(89, "Yoga Mat", "6 mm non-slip mat.", 35.00M, 10, "p-89.jpg",                                             3, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0),
        P(90, "Yoga Blocks", "Two cork blocks.", 22.00M, 10, "p-90.jpg",                                            1, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0),
        P(91, "Yoga Strap", "Cotton strap with a metal buckle.", 9.00M, 10, "p-91.jpg",                             1, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0),
        P(92, "Meditation Cushion", "Round cushion filled with buckwheat.", 39.00M, 10, "p-92.jpg",                  0, 0, 0, 1, 0, 0, 0, 0, 0, 9, 0),
        P(93, "Yoga Leggings", "High waist stretch leggings.", 45.00M, 10, "p-93.jpg",                               1, 0, 0, 0, 1, 0, 0, 0, 0, 7, 6),
        P(94, "Yoga Wheel", "Wheel for backbends and stretching.", 29.00M, 10, "p-94.jpg",                           1, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0),
        P(95, "Pilates Ball", "65 cm anti-burst ball.", 24.00M, 10, "p-95.jpg",                                     4, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0),

        // Clothing
        P(96, "Sport T-Shirt", "Moisture wicking t-shirt.", 19.00M, 11, "p-96.jpg",                                  3, 0, 1, 0, 3, 1, 1, 0, 0, 1, 9),
        P(97, "Hoodie", "Warm cotton hoodie.", 49.00M, 11, "p-97.jpg",                                              2, 0, 0, 1, 1, 0, 1, 0, 2, 0, 10),
        P(98, "Track Pants", "Tapered pants with zip pockets.", 39.00M, 11, "p-98.jpg",                             2, 0, 0, 0, 3, 1, 1, 0, 0, 1, 9),
        P(99, "Sports Bra", "Medium support sports bra.", 29.00M, 11, "p-99.jpg",                                    3, 0, 0, 0, 3, 0, 0, 0, 0, 4, 9),
        P(100, "Sport Cap", "Light cap for sunny days.", 15.00M, 11, "p-100.jpg",                                    0, 0, 2, 0, 2, 0, 0, 1, 2, 0, 8)
    };

    public static IEnumerable<Product> Products => Items.Select(i => i.Product);

    // every product gets a row for every category (zeros included), so the vector is complete
    public static IEnumerable<ProductWeight> ProductWeights =>
        Items.SelectMany(i => i.Vector.Select((weight, index) => new ProductWeight
        {
            IdProduct = i.Product.IdProduct,
            IdProductType = ProductTypes[index].IdProductType,
            Weight = weight
        }));

    private static (Product, double[]) P(int id, string name, string description, decimal price, int typeId, string photo, params double[] vector)
    {
        if (vector.Length != ProductTypes.Length)
            throw new InvalidOperationException($"Product {id} must have {ProductTypes.Length} weights.");

        var product = new Product
        {
            IdProduct = id, Name = name, Description = description, Availability = true,
            Price = price, PhotoName = photo, IdProductType = typeId
        };
        return (product, vector);
    }
}
