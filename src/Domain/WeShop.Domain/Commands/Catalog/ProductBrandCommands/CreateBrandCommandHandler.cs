using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WeShop.Domain.Abstract;
using WeShop.Domain.Entities;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductBrandCommands
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Result<string>>
    {
        private readonly IRepository<ProductBrand> _brandRepo;
        private readonly ILogger _logger;

        public CreateBrandCommandHandler(
            IRepository<ProductBrand> brandRepo,
            ILoggerFactory loggerFactory)
        {
            _brandRepo = brandRepo;
            _logger = loggerFactory.CreateLogger<CreateBrandCommand>();
        }

        public async Task<Result<string>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _logger.LogInformation($"{nameof(CreateBrandCommand)} 参数验证失败。");
                return Result.Fail<string>(request.GetErrors());
            }

            // 逻辑验证
            var isExit = await _brandRepo.AnyAsync(x => x.Name == request.BrandName);
            if (isExit)
            {
                _logger.LogInformation($"品牌 '{request.BrandName}' 已经存在。");
                return Result.Fail<string>($"'{request.BrandName}' 已经存在。");
            }

            var newBrand = new ProductBrand(request.BrandName);
            _brandRepo.Insert(newBrand);
            return await _brandRepo.UnitOfWork.SaveChangesAsync(cancellationToken) > 0
                ? Result.Ok(newBrand.Id.ToString())
                : Result.Fail<string>(string.Empty);
        }
    }
}