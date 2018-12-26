using FluentValidation;
using WeShop.Domain.Commands.Catalog.ProductCommands;

namespace WeShop.Domain.Validates.Catalog
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .Must(i => i > 0)
                .WithMessage("无效产品ID。");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("产品名称不能为空。");

            RuleFor(x => x.Price)
                .NotNull()
                .Must(a => a >= 0.0m)
                .WithMessage("产品价格必须大于 0.0。");

            RuleFor(x => x.Description)
                .MaximumLength(100)
                .WithMessage("产品简介不能超过 100 字符。");

            RuleFor(x => x.ProductBrandId)
                .Must(p => p > 0)
                .WithMessage("无效的产品品牌ID。");

            RuleFor(x => x.ProductTypeId)
                .Must(p => p > 0)
                .WithMessage("无效的产品类型ID。");
        }
    }
}