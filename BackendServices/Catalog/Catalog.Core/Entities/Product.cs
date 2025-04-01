namespace Catalog.Core.Entities;

public class Product : BaseEntity
{
    public string Sku { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal SellingPrice { get; set; }
    public string? ImageUrl { get; set; }

    public int BrandId { get; set; }
    public Brand Brand { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}