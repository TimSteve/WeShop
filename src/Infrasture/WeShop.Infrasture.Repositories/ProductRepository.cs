using System.Linq;
using WeShop.Domain.Entities;
using WeShop.Domain.Specifications;
using WeShop.Infrasture.Data;

namespace WeShop.Infrasture.Repositories
{
    public class ProductRepository : BaseRepository<Domain.Entities.Product>, IProductRepository
    {
        public ProductRepository(WeShopDbContext context) : base(context)
        {
        }

        public IQueryable<ProductBrand> GetBrands(int pageIndex = 1, int pageSize = 10)
        {
            var skipCount = GetSkipCount(pageIndex, pageSize);
            return _context.ProductBrands
                .Skip(skipCount)
                .Take(pageSize)
                .ShouldBeActive();
        }

        public IQueryable<ProductType> GetTypes(int pageIndex = 1, int pageSize = 10)
        {
            var skipCount = GetSkipCount(pageIndex, pageSize);
            return _context.ProductTypes
                .Skip(skipCount)
                .Take(pageSize)
                .ShouldBeActive();
        }

        public IQueryable<Domain.Entities.Product> GetProducts(int pageIndex = 1, int pageSize = 10)
        {
            var skipCount = GetSkipCount(pageIndex, pageSize);
            return _context.Products
                .Skip(skipCount)
                .Take(pageSize)
                .ShouldBeActive();
        }

        public ProductBrand GetBrand(long brandId)
        {
            return _context.ProductBrands
                .ShouldBeActive()
                .FirstOrDefault(x => x.Id == brandId);
        }

        public ProductType GetType(long typeId)
        {
            return _context.ProductTypes
                .ShouldBeActive()
                .FirstOrDefault(x => x.Id == typeId);
        }

        public Domain.Entities.Product GetProduct(long productId)
        {
            return _context.Products
                .AddInclude()
                .ShouldBeActive()
                .FirstOrDefault(x => x.Id == productId);
        }

        private int GetSkipCount(int pageIndex, int pageSize)
        {
            return (pageIndex - 1) * pageSize;
        }
    }
}