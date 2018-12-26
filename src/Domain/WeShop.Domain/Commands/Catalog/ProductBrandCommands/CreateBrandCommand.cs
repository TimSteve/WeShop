using FluentValidation;
using MediatR;
using WeShop.Domain.Abstract;
using WeShop.Domain.Validates.Catalog;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductBrandCommands
{
    public class CreateBrandCommand : BaseValidator, IRequest<Result<string>>
    {
        public string BrandName { get; private set; }

        public CreateBrandCommand(string brandName)
        {
            BrandName = brandName;
        }

        public override bool IsValid()
        {
            var validationResult = new CreateBrandCommandValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}