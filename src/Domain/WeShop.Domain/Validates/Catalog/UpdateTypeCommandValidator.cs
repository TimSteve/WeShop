using FluentValidation;
using WeShop.Domain.Commands.Catalog.ProductTypeCommands;

namespace WeShop.Domain.Validates.Catalog
{
    public class UpdateTypeCommandValidator : AbstractValidator<UpdateTypeCommand>
    {
        public UpdateTypeCommandValidator()
        {
            RuleFor(x => x.TypeId)
                .NotNull()
                .NotEmpty()
                .Must(x => x > 0)
                .WithMessage("无效的类型ID。");

            RuleFor(x => x.TypeName)
                .NotNull()
                .NotEmpty()
                .WithMessage("类型不能为空。");

            RuleFor(x => x.TypeName)
                .MaximumLength(50)
                .WithMessage("类型的长度不能超过 50 字符。");
        }
    }
}