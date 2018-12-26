using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using WeShop.Domain.Entities;
using WeShop.Domain.Specifications;
using WeShop.Queries.Dtos;

namespace WeShop.Queries
{
    public class ProductQueries : IProductQueries
    {
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;

        public ProductQueries(
            IProductRepository productRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public List<BrandDto> GetBrands(int pageIndex = 1, int pageSize = 10)
        {
            return _productRepo.GetBrands(pageIndex, pageSize)
                .ProjectTo<BrandDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public List<CategoryDto> GetTypes(int pageIndex = 1, int pageSize = 10)
        {
            return _productRepo.GetTypes(pageIndex, pageSize)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public List<ProductDto> GetProducts(int pageIndex = 1, int pageSize = 10)
        {
            return _productRepo.GetProducts(pageIndex, pageSize)
                .AddInclude()
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public BrandDto GetBrand(long brandId)
        {
            var brand = _productRepo.GetBrand(brandId);
            return _mapper.Map<ProductBrand, BrandDto>(brand);
        }

        public CategoryDto GetType(long typeId)
        {
            var type = _productRepo.GetType(typeId);
            return _mapper.Map<ProductType, CategoryDto>(type);
        }

        public ProductDto GetProduct(long productId)
        {
            var product = _productRepo.GetProduct(productId);
            return _mapper.Map<Product, ProductDto>(product);
        }
    }
}