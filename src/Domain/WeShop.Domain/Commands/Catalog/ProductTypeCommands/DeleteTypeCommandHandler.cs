using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WeShop.Domain.Abstract;
using WeShop.Domain.Entities;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductTypeCommands
{
    public class DeleteTypeCommandHandler : IRequestHandler<DeleteTypeCommand, Result>
    {
        private readonly IRepository<ProductType> _typeRepo;
        private readonly ILogger _logger;

        public DeleteTypeCommandHandler(
            IRepository<ProductType> typeRepo,
            ILoggerFactory loggerFactory)
        {
            _typeRepo = typeRepo;
            _logger = loggerFactory.CreateLogger<DeleteTypeCommandHandler>();
        }

        public async Task<Result> Handle(DeleteTypeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _logger.LogInformation($"{nameof(DeleteTypeCommand)} 参数验证失败。");
                return Result.Fail<string>(request.GetErrors());
            }

            var type = await _typeRepo.GetAsync(request.TypeId);
            if (type == null || type.IsDeleted)
            {
                _logger.LogWarning($"找不到 '{request.TypeId}' 相关产品类型。");
                return Result.Fail<string>("该类型不存在或已经被删除。");
            }

            type.SetDelete(true);
            _typeRepo.Update(type);
            return await _typeRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken)
                ? Result.Ok()
                : Result.Fail(string.Empty);
        }
    }
}