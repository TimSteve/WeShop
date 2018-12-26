using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WeShop.Domain.Abstract;
using WeShop.Domain.Entities;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductBrandCommands
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Result>
    {
        private readonly IRepository<ProductBrand> _brandRepo;
        private readonly ILogger _logger;

        public UpdateBrandCommandHandler(
            IRepository<ProductBrand> brandRepo,
            ILoggerFactory loggerFactory)
        {
            _brandRepo = brandRepo;
            _logger = loggerFactory.CreateLogger<UpdateBrandCommandHandler>();
        }

        public async Task<Result> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _logger.LogInformation($"{nameof(UpdateBrandCommand)} 参数验证失败。");
                return Result.Fail<string>(request.GetErrors());
            }

            var brand = await _brandRepo.GetAsync(request.BrandId);
            if (brand == null)
            {
                _logger.LogWarning($"找不到有关 '{brand.Id}' 数据。");
                return Result.Fail("抱歉，找不到要修改的品牌。");
            }

            var isExist = await _brandRepo.AnyAsync(x => x.Name == request.BrandName);
            if (isExist)
            {
                _logger.LogWarning($"'{request.BrandName}' 已存在，请勿重复修改。");
                return Result.Fail("该品牌名已存在，请勿重复修改。");
            }

            brand.SetName(request.BrandName);
            _brandRepo.Update(brand);
            return await _brandRepo.UnitOfWork.SaveChangesAsync(cancellationToken) > 0
                ? Result.Ok()
                : Result.Fail(string.Empty);
        }
    }
}