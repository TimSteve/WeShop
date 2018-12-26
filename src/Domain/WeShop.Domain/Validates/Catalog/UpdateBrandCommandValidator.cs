using FluentValidation;
using WeShop.Domain.Commands.Catalog.ProductBrandCommands;

namespace WeShop.Domain.Validates.Catalog
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
        {
            ValidateBrandId();
            ValidateName();
        }

        private void ValidateBrandId()
        {
            RuleFor(x => x.BrandId)
                .NotNull()
                .NotEmpty()
                .Must(x => x > 0)
                .WithMessage("无效的品牌ID。");
        }

        private void ValidateName()
        {
            RuleFor(x => x.BrandName)
                .NotNull()
                .NotEmpty()
                .WithMessage("品牌名称不能为空。");

            RuleFor(x => x.BrandName)
                .MaximumLength(100)
                .WithMessage("品牌名称的长度不能超过 100 字符。");
        }
    }
}