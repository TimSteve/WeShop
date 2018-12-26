using FluentValidation;
using MediatR;
using WeShop.Domain.Abstract;
using WeShop.Domain.Validates.Catalog;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductCommands
{
    public class DeleteProductCommand : BaseValidator, IRequest<Result>
    {
        public long ProductId { get; private set; }

        public DeleteProductCommand(long productId)
        {
            ProductId = productId;
        }

        public override bool IsValid()
        {
            var validatorResult = new DeleteProductCommandValidator().Validate(this);
            return validatorResult.IsValid;
        }
    }
}