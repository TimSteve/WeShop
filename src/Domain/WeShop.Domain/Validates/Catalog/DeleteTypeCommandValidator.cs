using FluentValidation;
using WeShop.Domain.Commands.Catalog.ProductTypeCommands;

namespace WeShop.Domain.Validates.Catalog
{
    public class DeleteTypeCommandValidator : AbstractValidator<DeleteTypeCommand>
    {
        public DeleteTypeCommandValidator()
        {
            RuleFor(x => x.TypeId)
                .NotNull()
                .NotEmpty()
                .Must(i => i > 0)
                .WithMessage("无效的类型ID。");
        }
    }
}