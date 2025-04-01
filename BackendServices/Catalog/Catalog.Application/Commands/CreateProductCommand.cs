using Catalog.Application.Responses;
using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Commands;

public class CreateProductCommand: IRequest<ProductResponse>
{
    public int Id { get; set; }

    public string Sku { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public decimal SellingPrice { get; set; }

    public int BrandId { get; set; }

    public int CategoryId { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}