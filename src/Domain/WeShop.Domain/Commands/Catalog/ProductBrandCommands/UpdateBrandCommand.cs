using FluentValidation;
using MediatR;
using WeShop.Domain.Abstract;
using WeShop.Domain.Validates.Catalog;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductBrandCommands
{
    public class UpdateBrandCommand :BaseValidator, IRequest<Result>
    {
        public long BrandId { get; private set; }

        public string BrandName { get; private set; }

        public UpdateBrandCommand(long brandId, string brandName)
        {
            BrandId = brandId;
            BrandName = brandName;
        }

        public override bool IsValid()
        {
            var validatorResult = new UpdateBrandCommandValidator().Validate(this);
            return validatorResult.IsValid;
        }
    }
}