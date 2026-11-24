namespace ProductCatalogMvc.Models;

/// <summary>Holds the products and filter values needed by the catalog page.</summary>
public class ProductIndexViewModel
{
    public IReadOnlyList<Product> Products { get; set; } = Array.Empty<Product>();

    public int? CategoryId { get; set; }

    public decimal? MinimumPrice { get; set; }

    public decimal? MaximumPrice { get; set; }

    public string? SearchTerm { get; set; }

    public string? SortOrder { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 6;

    public int TotalItems { get; set; }

    public int TotalPages => Math.Max(1, (int)Math.Ceiling((double)TotalItems / PageSize));

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;
}
