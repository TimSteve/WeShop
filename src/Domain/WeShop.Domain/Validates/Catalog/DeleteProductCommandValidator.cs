using FluentValidation;
using WeShop.Domain.Commands.Catalog.ProductCommands;

namespace WeShop.Domain.Validates.Catalog
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotNull()
                .NotEmpty()
                .Must(i => i > 0)
                .WithMessage("无效的产品ID。");
        }
    }
}