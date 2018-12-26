using System.Linq;
using WeShop.Domain.Abstract;

namespace WeShop.Domain.Entities
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<ProductBrand> GetBrands(int pageIndex = 1, int pageSize = 10);
        IQueryable<ProductType> GetTypes(int pageIndex = 1, int pageSize = 10);
        IQueryable<Product> GetProducts(int pageIndex = 1, int pageSize = 10);
        ProductBrand GetBrand(long brandId);
        ProductType GetType(long typeId);
        Product GetProduct(long productId);
    }
}