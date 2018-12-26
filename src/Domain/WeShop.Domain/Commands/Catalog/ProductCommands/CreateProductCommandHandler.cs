using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WeShop.Domain.Abstract;
using WeShop.Domain.Entities;
using WeShop.Domain.Services;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductCommands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<string>>
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IProductDomainService _productDomainService;
        private readonly ILogger _logger;

        public CreateProductCommandHandler(
            IRepository<Product> productRepo,
            IProductDomainService productDomainService,
            ILoggerFactory loggerFactory)
        {
            _productRepo = productRepo;
            _productDomainService = productDomainService;
            _logger = loggerFactory.CreateLogger<CreateProductCommandHandler>();
        }

        public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _logger.LogInformation($"{nameof(CreateProductCommand)} 参数验证失败。");
                return Result.Fail<string>(request.GetErrors());
            }

            var product = new Product(request.Name, request.Description, request.Price, request.PictureFileName,
                request.PictureUri, request.ProductBrandId, request.ProductTypeId, request.IsPublished);
            var result = await _productDomainService.PreInsertAsync(product);
            if (!result.Success)
            {
                return Result.Fail<string>(result.Error);
            }

            _productRepo.Insert(product);
            return await _productRepo.UnitOfWork.SaveChangesAsync(cancellationToken) > 0
                ? Result.Ok(product.Id.ToString())
                : Result.Fail<string>(string.Empty);
        }
    }
}