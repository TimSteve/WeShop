using System.Collections.Generic;
using WeShop.Queries.Dtos;

namespace WeShop.Queries
{
    public interface IProductQueries
    {
        List<BrandDto> GetBrands(int pageIndex = 1, int pageSize = 10);
        List<CategoryDto> GetTypes(int pageIndex = 1, int pageSize = 10);
        List<ProductDto> GetProducts(int pageIndex = 1, int pageSize = 10);
        BrandDto GetBrand(long brandId);
        CategoryDto GetType(long typeId);
        ProductDto GetProduct(long productId);
    }
}