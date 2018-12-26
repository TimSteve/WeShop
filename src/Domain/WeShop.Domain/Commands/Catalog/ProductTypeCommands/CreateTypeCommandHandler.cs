using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WeShop.Domain.Abstract;
using WeShop.Domain.Entities;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductTypeCommands
{
    public class CreateTypeCommandHandler : IRequestHandler<CreateTypeCommand, Result<string>>
    {
        private readonly IRepository<ProductType> _typeRepo;
        private readonly ILogger _logger;

        public CreateTypeCommandHandler(IRepository<ProductType> typeRepo, ILoggerFactory loggerFactory)
        {
            _typeRepo = typeRepo;
            _logger = loggerFactory.CreateLogger<CreateTypeCommandHandler>();
        }

        public async Task<Result<string>> Handle(CreateTypeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _logger.LogInformation($"{nameof(CreateTypeCommand)} 参数验证失败。");
                return Result.Fail<string>(request.GetErrors());
            }

            var isExit = await _typeRepo.AnyAsync(x => x.Name == request.TypeName);
            if (isExit)
            {
                return Result.Fail<string>("该类型已存在，请勿重复添加。");
            }

            var type = new ProductType(request.TypeName);
            _typeRepo.Insert(type);
            return await _typeRepo.UnitOfWork.SaveChangesAsync(cancellationToken) > 0
                ? Result.Ok(type.Id.ToString())
                : Result.Fail<string>(string.Empty);
        }
    }
}