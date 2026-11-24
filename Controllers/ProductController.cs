using Microsoft.AspNetCore.Mvc;
using ProductCatalogMvc.Data;
using ProductCatalogMvc.Models;

namespace ProductCatalogMvc.Controllers;

public class ProductController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        // Preserve a message carried here by Reset or the missing-product redirect.
        if (TempData.Peek("StatusMessage") is null && TempData.Peek("ErrorMessage") is null)
        {
            TempData["StatusMessage"] = "The product catalog loaded successfully.";
        }

        var products = ProductCatalogData.Products
            .Select(AddCategory)
            .OrderBy(product => product.Name)
            .ToList();

        return ShowIndex(products, new ProductIndexViewModel());
    }

    [HttpGet]
    public IActionResult Reset()
    {
        TempData["StatusMessage"] = "All filters have been reset.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Filter(
        int? categoryId,
        decimal? minimumPrice,
        decimal? maximumPrice,
        string? searchTerm,
        string? sortOrder,
        int page = 1)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Please enter valid numbers for the category, prices, and page.";
        }

        if (minimumPrice < 0 || maximumPrice < 0)
        {
            TempData["ErrorMessage"] = "Prices cannot be negative. The price filters were cleared.";
            minimumPrice = null;
            maximumPrice = null;
        }

        if (minimumPrice.HasValue && maximumPrice.HasValue && minimumPrice > maximumPrice)
        {
            TempData["ErrorMessage"] = "The minimum price must be less than or equal to the maximum price.";
            minimumPrice = null;
            maximumPrice = null;
        }

        if (categoryId.HasValue && ProductCatalogData.FindCategory(categoryId.Value) is null)
        {
            TempData["ErrorMessage"] = "That category is unavailable, so all categories are shown.";
            categoryId = null;
        }

        IEnumerable<Product> query = ProductCatalogData.Products.Select(AddCategory);

        if (categoryId.HasValue)
        {
            query = query.Where(product => product.CategoryId == categoryId.Value);
        }

        if (minimumPrice.HasValue)
        {
            query = query.Where(product => product.Price >= minimumPrice.Value);
        }

        if (maximumPrice.HasValue)
        {
            query = query.Where(product => product.Price <= maximumPrice.Value);
        }

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var search = searchTerm.Trim();
            query = query.Where(product =>
                product.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                product.Description.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                product.Category!.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            searchTerm = search;
        }

        query = sortOrder switch
        {
            "name" => query.OrderBy(product => product.Name),
            "price" => query.OrderBy(product => product.Price),
            "price_desc" => query.OrderByDescending(product => product.Price),
            _ => query.OrderBy(product => product.Name)
        };

        var products = query.ToList();
        var filterWasUsed = categoryId.HasValue || minimumPrice.HasValue || maximumPrice.HasValue ||
                            !string.IsNullOrWhiteSpace(searchTerm);

        if (filterWasUsed && TempData.Peek("ErrorMessage") is null)
        {
            TempData["StatusMessage"] = $"Filter applied. {products.Count} product(s) found.";
        }

        var model = new ProductIndexViewModel
        {
            Products = products,
            CategoryId = categoryId,
            MinimumPrice = minimumPrice,
            MaximumPrice = maximumPrice,
            SearchTerm = searchTerm,
            SortOrder = sortOrder,
            PageNumber = page
        };

        return ShowIndex(products, model);
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var product = ProductCatalogData.FindProduct(id);
        if (product is null)
        {
            TempData["ErrorMessage"] = "We couldn't find that product. It may have been removed.";
            return RedirectToAction(nameof(Index));
        }

        TempData["StatusMessage"] = "Product details loaded successfully.";
        return View(AddCategory(product));
    }

    private IActionResult ShowIndex(IReadOnlyList<Product> products, ProductIndexViewModel model)
    {
        // Demonstrates the four data-passing options required by the assignment:
        // Model for products, ViewBag for the page title/greeting/statistics, ViewData for categories,
        // and TempData for messages shown during filtering or after a redirect.
        // Paginate only after filtering and sorting, so each page belongs to the same result set.
        model.PageSize = 6;
        model.TotalItems = products.Count;
        model.PageNumber = Math.Clamp(model.PageNumber, 1, Math.Max(1, model.TotalPages));
        model.Products = products
            .Skip((model.PageNumber - 1) * model.PageSize)
            .Take(model.PageSize)
            .ToList();
        ViewBag.PageTitle = "Product catalog";
        ViewBag.WelcomeMessage = "Browse the catalog, find a good match, and check the details.";
        ViewBag.TotalProducts = ProductCatalogData.Products.Count;
        ViewBag.TotalValue = ProductCatalogData.Products.Sum(product => product.Price);
        ViewBag.AveragePrice = ProductCatalogData.Products.Average(product => product.Price);
        ViewData["Categories"] = ProductCatalogData.Categories;

        return View("Index", model);
    }

    private static Product AddCategory(Product product)
    {
        // Return a copy so handling a request doesn't change shared sample objects.
        return new Product
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            CategoryId = product.CategoryId,
            Category = ProductCatalogData.FindCategory(product.CategoryId)
        };
    }
}
