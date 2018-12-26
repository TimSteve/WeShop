using AutoMapper;
using WeShop.Domain.Entities;
using WeShop.Queries.Dtos;

namespace WeShop.Queries.MapperProfiles
{
    public class ProductBrandProfile : Profile
    {
        public ProductBrandProfile()
        {
            CreateMap<ProductBrand, BrandDto>()
                .ForMember(d => d.BrandId, s => s.MapFrom(m => m.Id.ToString()))
                .ForMember(d => d.BrandName, s => s.MapFrom(m => m.Name));
        }
    }
}