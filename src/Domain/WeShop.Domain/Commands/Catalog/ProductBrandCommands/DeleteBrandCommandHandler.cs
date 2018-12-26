using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WeShop.Domain.Abstract;
using WeShop.Domain.Entities;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductBrandCommands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result>
    {
        private readonly IRepository<ProductBrand> _brandRepo;
        private readonly ILogger _logger;

        public DeleteBrandCommandHandler(
            IRepository<ProductBrand> brandRepo,
            ILoggerFactory loggerFactory)
        {
            _brandRepo = brandRepo;
            _logger = loggerFactory.CreateLogger<DeleteBrandCommandHandler>();
        }

        public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _logger.LogInformation($"{nameof(DeleteBrandCommand)} 参数验证失败。");
                return Result.Fail<string>(request.GetErrors());
            }

            var brand = await _brandRepo.GetAsync(request.BrandId);
            if (brand == null || brand.IsDeleted)
            {
                _logger.LogWarning($"找不到要修改的品牌: {request.BrandId}");
                return Result.Fail("抱歉，找不到要修改的数据。");
            }

            brand.SetDelete(true);
            _brandRepo.Update(brand);
            return await _brandRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken)
                ? Result.Ok()
                : Result.Fail(string.Empty);
        }
    }
}