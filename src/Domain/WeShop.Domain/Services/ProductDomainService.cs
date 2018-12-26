using System.Threading.Tasks;
using WeShop.Domain.Abstract;
using WeShop.Domain.Entities;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Services
{
    public class ProductDomainService : IProductDomainService
    {
        public ProductDomainService(IRepository<Product> productRepo, IRepository<ProductBrand> brandRepo,
            IRepository<ProductType> typeRepo)
        {
            _productRepo = productRepo;
            _brandRepo = brandRepo;
            _typeRepo = typeRepo;
        }

        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<ProductBrand> _brandRepo;
        private readonly IRepository<ProductType> _typeRepo;

        public async Task<Result> PreInsertAsync(Product product)
        {
            return await PreInsertOrUpdateProductAsync(product);
        }

        public async Task<Result> PreUpdateAsync(Product product)
        {
            return await PreInsertOrUpdateProductAsync(product, false);
        }

        private async Task<Result> PreInsertOrUpdateProductAsync(Product product, bool isInsert = true)
        {
            var anyName = await _productRepo.AnyAsync(x => x.Name == product.Name);
            if (anyName)
                return Result.Fail<string>("该产品已经存在，请勿重复添加。");

            var anyBrand = await _brandRepo.AnyAsync(x => x.Id == product.ProductBrandId);
            if (!anyBrand)
                return Result.Fail<string>("无效的品牌。");

            var anyType = await _typeRepo.AnyAsync(x => x.Id == product.ProductTypeId);
            if (!anyType)
                return Result.Fail<string>("无效的类型。");

            return Result.Ok();
        }
    }
}