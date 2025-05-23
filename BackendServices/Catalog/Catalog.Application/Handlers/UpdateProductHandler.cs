using Catalog.Application.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = await _productRepository.UpdateProduct(new Product
        {
            Id = request.Id,
            Sku = request.Sku,
            Name = request.Name,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            SellingPrice = request.SellingPrice,
            BrandId = request.BrandId,
            CategoryId = request.CategoryId
        });

        return productEntity;
    }

}