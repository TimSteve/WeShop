using System.Linq;
using Microsoft.EntityFrameworkCore;
using WeShop.Domain.Entities;

namespace WeShop.Domain.Specifications
{
    public static class ProductSpecifications
    {
        public static IQueryable<Product> ShouldBeActive(this IQueryable<Product> products)
        {
            return products.Where(x => x.IsDeleted == false);
        }

        public static IQueryable<Product> ShouldBePublish(this IQueryable<Product> products)
        {
            return products.Where(x => x.IsPublished);
        }

        public static IQueryable<Product> AddInclude(this IQueryable<Product> products)
        {
            return products.Include(x => x.ProductBrand)
                .Include(x => x.ProductType);
        }
    }
}