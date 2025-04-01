using System.Net;
using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using Common.Logging;
using Common.Logging.Correlation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

public class CatalogController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ILogger<CatalogController> _logger;
    private readonly ICorrelationIdGenerator _correlationIdGenerator;

    public CatalogController(IMediator mediator, ILogger<CatalogController> logger, ICorrelationIdGenerator correlationIdGenerator)
    {
        _mediator = mediator;
        _logger = logger;
        _correlationIdGenerator = correlationIdGenerator;
        _logger.LogInformation("CorrelationId {correlationId}:", _correlationIdGenerator.Get());
    }

    [HttpGet]
    [Route("[action]/{id:int}", Name = "product-by-id")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductResponse>> GetProductById(int id) // <-- CAMBIO a int
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("[action]/{sku}", Name = "product-by-sku")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductResponse>> GetProductBySku(string sku) // <-- CAMBIO a int
    {
        var query = new GetProductBySkuQuery(sku);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("[action]/{productName}", Name = "product-by-name")]
    [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetProductByProductName(string productName)
    {
        var query = new GetProductByNameQuery(productName);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("products")]
    [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
    {
        try
        {
            var token = Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(token))
        {
            _logger.LogWarning("No Authorization header found");
        }
        else
        {
            _logger.LogInformation("Authorization header: {token}", token);
        }

            var query = new GetAllProductsQuery(catalogSpecParams);
            var result = await _mediator.Send(query);
            _logger.LogInformation("All products retrieved");
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An Exception has occured: {Exception}");
            throw;
        }
    }

    [HttpGet]
    [Route("brands")]
    [ProducesResponseType(typeof(IList<BrandResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<BrandResponse>>> GetAllBrands()
    {
        var query = new GetAllBrandsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("categories")]
    [ProducesResponseType(typeof(IList<CategoryResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<CategoryResponse>>> GetAllTypes()
    {
        var token = Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(token))
        {
            _logger.LogWarning("No Authorization header found");
        }
        else
        {
            _logger.LogInformation("Authorization header: {token}", token);
        }

        var query = new GetAllTypesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("[action]/{brand}", Name = "product-by-brand-name")]
    [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetProductsByBrandName(string brand)
    {
        var query = new GetProductByBrandQuery(brand);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Route("product")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand productCommand)
    {
        var result = await _mediator.Send(productCommand);
        return Ok(result);
    }

    [HttpPut]
    [Route("product")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand productCommand)
    {
        var result = await _mediator.Send(productCommand);
        return Ok(result);
    }
    [HttpDelete]
    [Route("{id:int}", Name = "product")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProduct(int id) // <-- CAMBIO a int
    {
        var query = new DeleteProductByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}