using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WeShop.Domain.Abstract;
using WeShop.Domain.Entities;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductTypeCommands
{
    public class UpdateTypeCommandHandler : IRequestHandler<UpdateTypeCommand, Result>
    {
        public UpdateTypeCommandHandler(
            IRepository<ProductType> typeRepo,
            ILoggerFactory loggerFactory)
        {
            _typeRepo = typeRepo;
            _logger = loggerFactory.CreateLogger<UpdateTypeCommandHandler>();
        }

        private readonly IRepository<ProductType> _typeRepo;
        private readonly ILogger _logger;

        public async Task<Result> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _logger.LogInformation($"{nameof(UpdateTypeCommand)} 参数验证失败。");
                return Result.Fail<string>(request.GetErrors());
            }

            var type = await _typeRepo.GetAsync(request.TypeId);
            if (type == null)
            {
                return Result.Fail("该类型不存在或已经被删除。");
            }

            type.SetName(request.TypeName);
            _typeRepo.Update(type);
            return await _typeRepo.UnitOfWork.SaveChangesAsync(cancellationToken) > 0
                ? Result.Ok()
                : Result.Fail(string.Empty);
        }
    }
}