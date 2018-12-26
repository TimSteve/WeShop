using FluentValidation;
using MediatR;
using WeShop.Domain.Abstract;
using WeShop.Domain.Validates.Catalog;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductTypeCommands
{
    public class CreateTypeCommand :BaseValidator, IRequest<Result<string>>
    {
        public string TypeName { get; set; }

        public CreateTypeCommand(string typeName)
        {
            TypeName = typeName;
        }

        public override bool IsValid()
        {
            var validatorResult = new CreateTypeCommandValidator().Validate(this);
            return validatorResult.IsValid;
        }
    }
}