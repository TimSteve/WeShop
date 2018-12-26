using AutoMapper;
using WeShop.Domain.Entities;
using WeShop.Queries.Dtos;

namespace WeShop.Queries.MapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductId, s => s.MapFrom(m => m.Id.ToString()))
                .ForMember(d => d.Name, s => s.MapFrom(m => m.Name))
                .ForMember(d => d.Description, s => s.MapFrom(m => m.Description))
                .ForMember(d => d.Price, s => s.MapFrom(m => m.Price))
                .ForMember(d => d.PictureFileName, s => s.MapFrom(m => m.PictureFileName))
                .ForMember(d => d.PictureUri, s => s.MapFrom(m => m.PictureUri))
                .ForMember(d => d.ProductBrand, s => s.MapFrom(m => m.ProductBrand))
                .ForMember(d => d.ProductType, s => s.MapFrom(m => m.ProductType));
        }
    }
}