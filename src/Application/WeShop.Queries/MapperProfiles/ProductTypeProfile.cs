using AutoMapper;
using WeShop.Domain.Entities;
using WeShop.Queries.Dtos;

namespace WeShop.Queries.MapperProfiles
{
    public class ProductTypeProfile : Profile
    {
        public ProductTypeProfile()
        {
            CreateMap<ProductType, CategoryDto>()
                .ForMember(d => d.CategoryId, s => s.MapFrom(m => m.Id.ToString()))
                .ForMember(d => d.CategoryName, s => s.MapFrom(m => m.Name));
        }
    }
}