using Catalog.Core.Entities;

namespace Catalog.Application.Responses;

public class ProductResponse
{
    public string Sku { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal SellingPrice { get; set; }
    public string? ImageUrl { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }

    public string BrandName { get; set; }
    public string CategoryName { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}