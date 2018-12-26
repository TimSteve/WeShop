using FluentValidation;
using MediatR;
using WeShop.Domain.Abstract;
using WeShop.Domain.Validates.Catalog;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductTypeCommands
{
    public class UpdateTypeCommand : BaseValidator, IRequest<Result>
    {
        public long TypeId { get; private set; }
        public string TypeName { get; private set; }

        public UpdateTypeCommand(long typeId, string typeName)
        {
            TypeId = typeId;
            TypeName = typeName;
        }

        public override bool IsValid()
        {
            var validatorResult = new UpdateTypeCommandValidator().Validate(this);
            return validatorResult.IsValid;
        }
    }
}