using FluentValidation;
using WeShop.Domain.Commands.Catalog.ProductBrandCommands;

namespace WeShop.Domain.Validates.Catalog
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
        }

        /// <summary>
        /// 验证品牌名
        /// </summary>
        private void ValidateBrandName()
        {
            RuleFor(x => x.BrandName)
                .NotNull()
                .NotEmpty()
                .WithMessage("品牌名不能为空。");

            RuleFor(x => x.BrandName)
                .MinimumLength(1)
                .MaximumLength(100)
                .WithMessage("品牌名在1-100字符之间。");
        }
    }
}