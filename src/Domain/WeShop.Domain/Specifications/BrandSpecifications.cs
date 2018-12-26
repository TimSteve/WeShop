using System.Linq;
using WeShop.Domain.Entities;

namespace WeShop.Domain.Specifications
{
    public static class ProductBrandSpecifications
    {
        public static IQueryable<ProductBrand> ShouldBeActive(this IQueryable<ProductBrand> brands)
        {
            return brands.Where(x => x.IsDeleted == false);
        }
    }
}