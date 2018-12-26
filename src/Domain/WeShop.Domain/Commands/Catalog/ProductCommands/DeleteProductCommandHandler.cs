using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WeShop.Domain.Abstract;
using WeShop.Domain.Entities;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductCommands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IRepository<Product> _productRepo;
        private readonly ILogger _logger;

        public DeleteProductCommandHandler(
            IRepository<Product> productRepo,
            ILoggerFactory loggerFactory)
        {
            _productRepo = productRepo;
            _logger = loggerFactory.CreateLogger<DeleteProductCommandHandler>();
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _logger.LogInformation($"{nameof(DeleteProductCommand)} 参数验证失败。");
                return Result.Fail<string>(request.GetErrors());
            }

            var product = await _productRepo.GetAsync(request.ProductId);
            if (product == null || product.IsDeleted)
            {
                _logger.LogError($"找不到 '{request.ProductId}' 相关产品，无法删除。");
                return Result.Fail("该产品不存在或已被删除。");
            }

            product.SetDelete(true);
            _productRepo.Update(product);
            return await _productRepo.UnitOfWork.SaveChangesAsync(cancellationToken) > 0
                ? Result.Ok()
                : Result.Fail(string.Empty);
        }
    }
}