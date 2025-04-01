using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Commands;

public class UpdateProductCommand : IRequest<bool>
{
    public int Id { get; set; } // Ahora es int, no string

    public string Sku { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public decimal SellingPrice { get; set; }

    public int BrandId { get; set; }

    public int CategoryId { get; set; }
}