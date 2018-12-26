using System.Linq;
using WeShop.Domain.Entities;

namespace WeShop.Domain.Specifications
{
    public static class ProductTypeSpecifications
    {
        public static IQueryable<ProductType> ShouldBeActive(this IQueryable<ProductType> productTypes)
        {
            return productTypes.Where(x => x.IsDeleted == false);
        }
    }
}