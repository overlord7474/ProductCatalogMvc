# Everyday Catalog (ASP.NET Core MVC)

A small beginner-friendly MVC project based on the attached class assignment. It demonstrates `Product` and `Category` models, controller actions, Razor views, filtering, search, sorting, statistics, and MVC's `Model`, `ViewBag`, `ViewData`, and `TempData`.

## Run it

1. Open `ProductCatalogMvc.csproj` in Visual Studio, or open this folder in a terminal.
2. Run:

   ```powershell
   dotnet run
   ```

3. Open the local URL printed by the command. The default page is the product catalog.

## What to look at first

- `Models/Product.cs` and `Models/Category.cs` define the data.
- `Data/ProductCatalogData.cs` contains the sample products and categories.
- `Controllers/ProductController.cs` contains `Index`, `Filter`, `Details`, and `Reset`.
- `Views/Product/Index.cshtml` renders the list, filters, status messages, and statistics.
- `Views/Product/Details.cshtml` renders one product.

## Assignment concepts used

- **Model:** the catalog page receives a `ProductIndexViewModel`; the details page receives one `Product`.
- **ViewBag:** the catalog page title, greeting, product count, total value, and average price are passed from the controller.
- **ViewData:** the category choices are passed to the filter dropdown.
- **TempData:** success messages are shown on the initial catalog, after filtering, on product details, and after resetting filters. Missing products redirect to the catalog with a friendly error.
- **Pagination:** six products appear per page. Previous, Next, and numbered links preserve the category, price, search, and sort selections. Applying new filters returns to page one.
- **Validation:** `Product` has data-annotation rules for required text, sensible lengths, positive price, and a selected category.

The products are sample data kept in memory. This keeps the exercise focused on MVC; the list resets when the application restarts, and there is no database or product-editing form.
