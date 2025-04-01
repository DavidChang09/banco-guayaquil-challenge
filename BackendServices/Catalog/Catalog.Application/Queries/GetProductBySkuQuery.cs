using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductBySkuQuery : IRequest<ProductResponse>
    {
        public string Sku { get; set; }

        public GetProductBySkuQuery(string sku)
        {
            Sku = sku;
        }
    }
}