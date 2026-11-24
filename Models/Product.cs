using System.ComponentModel.DataAnnotations;

namespace ProductCatalogMvc.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Enter a product name.")]
    [StringLength(80, ErrorMessage = "The product name must be 80 characters or fewer.")]
    public string Name { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "100000000", ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Enter a short product description.")]
    [StringLength(300, ErrorMessage = "The description must be 300 characters or fewer.")]
    public string Description { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Choose a product category.")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}
