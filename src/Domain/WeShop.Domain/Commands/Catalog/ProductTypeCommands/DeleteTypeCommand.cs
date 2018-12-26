using MediatR;
using WeShop.Domain.Abstract;
using WeShop.Domain.Validates.Catalog;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductTypeCommands
{
    public class DeleteTypeCommand : BaseValidator, IRequest<Result>
    {
        public long TypeId { get; private set; }

        public DeleteTypeCommand(long typeId)
        {
            TypeId = typeId;
        }

        public override bool IsValid()
        {
            var validatorResult = new DeleteTypeCommandValidator().Validate(this);
            return validatorResult.IsValid;
        }
    }
}