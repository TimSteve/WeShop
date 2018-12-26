using FluentValidation;
using WeShop.Domain.Commands.Catalog.ProductBrandCommands;

namespace WeShop.Domain.Validates.Catalog
{
    public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
    {
        public DeleteBrandCommandValidator()
        {
            ValidateBrandId();
        }

        private void ValidateBrandId()
        {
            RuleFor(x => x.BrandId)
                .NotNull()
                .NotEmpty()
                .Must(i => i > 0)
                .WithMessage("无效的品牌ID。");
        }
    }
}