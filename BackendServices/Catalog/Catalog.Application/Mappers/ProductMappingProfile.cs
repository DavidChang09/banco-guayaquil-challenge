using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Specs;

namespace Catalog.Application.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductResponse>()
        .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<Brand, BrandResponse>().ReverseMap();
        CreateMap<Category, CategoryResponse>().ReverseMap();
        CreateMap<Pagination<Product>, Pagination<ProductResponse>>().ReverseMap();
    }
}