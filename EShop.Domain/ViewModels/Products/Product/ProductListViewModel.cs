using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.ViewModels.Products.Product;

public class ProductListViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Price { get; set; }
    public string? ImageName { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? CategoryTitle { get; set; } 
}