using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WeShop.Domain.Abstract;
using WeShop.Domain.Entities;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductCommands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly IRepository<Product> _productRepo;
        private readonly ILogger _logger;

        public UpdateProductCommandHandler(
            IRepository<Product> productRepo,
            ILoggerFactory loggerFactory)
        {
            _productRepo = productRepo;
            _logger = loggerFactory.CreateLogger<UpdateProductCommandHandler>();
        }

        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _logger.LogInformation($"{nameof(UpdateProductCommand)} 参数验证失败。");
                return Result.Fail<string>(request.GetErrors());
            }

            var product = await _productRepo.GetAsync(request.ProductId);
            if (product == null)
            {
                _logger.LogWarning($"找不到有关 '{request.ProductId}' 数据。");
                return Result.Fail("抱歉，找不到要修改的品牌。");
            }

            product.BeUpdate(request.Name, request.Description, request.Price, request.PictureUri,
                request.PictureFileName, request.ProductBrandId, request.ProductTypeId);
            if (product.IsDeleted != request.IsDeleted)
                product.SetDelete(request.IsDeleted);
            if (product.IsPublished != request.IsPublished)
                product.SetPublish(request.IsPublished);

            _productRepo.Update(product);
            return await _productRepo.UnitOfWork.SaveChangesAsync(cancellationToken) > 0
                ? Result.Ok()
                : Result.Fail(string.Empty);
        }
    }
}