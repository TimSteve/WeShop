using FluentValidation;
using WeShop.Domain.Commands.Catalog.ProductTypeCommands;

namespace WeShop.Domain.Validates.Catalog
{
    public class CreateTypeCommandValidator : AbstractValidator<CreateTypeCommand>
    {
        public CreateTypeCommandValidator()
        {
            RuleFor(x => x.TypeName)
                .NotNull()
                .NotEmpty()
                .WithMessage("产品类型不能为空。");
        }
    }
}