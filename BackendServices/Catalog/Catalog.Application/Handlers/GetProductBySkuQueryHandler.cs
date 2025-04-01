using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductBySkuQueryHandler : IRequestHandler<GetProductBySkuQuery, ProductResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductBySkuQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ProductResponse> Handle(GetProductBySkuQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductBySku(request.Sku);
        var productResponse = ProductMapper.Mapper.Map<ProductResponse>(product);
        return productResponse;
    }
}