using FluentValidation;
using MediatR;
using WeShop.Domain.Abstract;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductBrandCommands
{
    public class DeleteBrandCommand :BaseValidator, IRequest<Result>
    {
        public long BrandId { get; private set; }

        public DeleteBrandCommand(long brandId)
        {
            BrandId = brandId;
        }

        public override bool IsValid()
        {
            var validatorResult = new DeleteBrandCommandValidator().Validate(this);
            return validatorResult.IsValid;
        }
    }

    public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
    {
        public DeleteBrandCommandValidator()
        {
            RuleFor(x => x.BrandId)
                .NotNull()
                .NotEmpty()
                .Must(i => i > 0)
                .WithMessage("无效的品牌ID。");
        }
    }
}