using ProductCatalogMvc.Models;

namespace ProductCatalogMvc.Data;

/// <summary>
/// Small in-memory sample catalog. It keeps this MVC exercise focused on
/// controllers and views instead of database setup.
/// </summary>
public static class ProductCatalogData
{
    public static IReadOnlyList<Category> Categories { get; } =
    [
        new Category { Id = 1, Name = "Electronics" },
        new Category { Id = 2, Name = "Home" },
        new Category { Id = 3, Name = "Books" },
        new Category { Id = 4, Name = "Accessories" }
    ];

    public static IReadOnlyList<Product> Products { get; } =
    [
        new Product { Id = 1, Name = "Wireless Headphones", Price = 89.99m, Description = "Comfortable over-ear headphones with clear sound and long battery life.", CategoryId = 1 },
        new Product { Id = 2, Name = "Mechanical Keyboard", Price = 129.00m, Description = "A sturdy keyboard with tactile switches for everyday work.", CategoryId = 1 },
        new Product { Id = 3, Name = "USB-C Hub", Price = 39.50m, Description = "Add useful ports to a laptop with one compact adapter.", CategoryId = 1 },
        new Product { Id = 4, Name = "Desk Lamp", Price = 45.00m, Description = "An adjustable lamp with a soft, focused light for your desk.", CategoryId = 2 },
        new Product { Id = 5, Name = "Coffee Grinder", Price = 74.95m, Description = "A compact burr grinder for fresh coffee at home.", CategoryId = 2 },
        new Product { Id = 6, Name = "Ceramic Mug", Price = 16.00m, Description = "A simple, durable mug for coffee, tea, or hot chocolate.", CategoryId = 2 },
        new Product { Id = 7, Name = "Clean Code", Price = 38.50m, Description = "A practical book about writing code that is easier to read and maintain.", CategoryId = 3 },
        new Product { Id = 8, Name = "The Pragmatic Programmer", Price = 42.00m, Description = "A collection of practical ideas for becoming a more effective developer.", CategoryId = 3 },
        new Product { Id = 9, Name = "Everyday Backpack", Price = 64.00m, Description = "A lightweight backpack with room for a laptop and daily essentials.", CategoryId = 4 },
        new Product { Id = 10, Name = "27-inch Monitor", Price = 229.99m, Description = "A sharp, spacious display that works well for study and office tasks.", CategoryId = 1 }
    ];

    public static Product? FindProduct(int id) => Products.FirstOrDefault(product => product.Id == id);

    public static Category? FindCategory(int id) => Categories.FirstOrDefault(category => category.Id == id);
}
