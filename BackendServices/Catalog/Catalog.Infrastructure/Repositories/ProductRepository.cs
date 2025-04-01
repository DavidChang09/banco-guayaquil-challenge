using Microsoft.EntityFrameworkCore;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
{
    private readonly CatalogDbContext _context;

    public ProductRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<Pagination<Product>> GetProducts(CatalogSpecParams catalogSpecParams)
    {
        var query = _context.Products
        .Include(p => p.Brand)
        .Include(p => p.Category)
        .AsQueryable();

        if (!string.IsNullOrEmpty(catalogSpecParams.Search))
            query = query.Where(x => x.Name.Contains(catalogSpecParams.Search));

        if (catalogSpecParams.BrandId.HasValue)
            query = query.Where(x => x.BrandId == catalogSpecParams.BrandId.Value);

        if (catalogSpecParams.TypeId.HasValue)
            query = query.Where(x => x.CategoryId == catalogSpecParams.TypeId.Value);

        if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
        {
            query = catalogSpecParams.Sort switch
            {
                "priceAsc" => query.OrderBy(x => x.SellingPrice),
                "priceDesc" => query.OrderByDescending(x => x.SellingPrice),
                _ => query.OrderBy(x => x.Name)
            };
        }

        var count = await query.CountAsync();

        var data = await query
            .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
            .Take(catalogSpecParams.PageSize)
            .ToListAsync();

        return new Pagination<Product>
        {
            PageSize = catalogSpecParams.PageSize,
            PageIndex = catalogSpecParams.PageIndex,
            Data = data,
            Count = count
        };
    }

    public async Task<Product> GetProduct(int id)
    {
        return await _context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> GetProductBySku(string sku)
    {
        return await _context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Sku == sku);
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
        return await _context.Products
            .Where(p => p.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByBrand(string brandName)
    {
        return await _context.Products
            .Include(p => p.Brand)
            .Where(p => p.Brand.Name == brandName)
            .ToListAsync();
    }

    public async Task<Product> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;
        _context.Products.Remove(product);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Brand>> GetAllBrands()
    {
        return await _context.Brands.ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetAllTypes()
    {
        return await _context.Categories.ToListAsync();
    }
}