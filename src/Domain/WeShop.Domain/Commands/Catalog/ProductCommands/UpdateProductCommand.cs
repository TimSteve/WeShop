using FluentValidation;
using MediatR;
using WeShop.Domain.Abstract;
using WeShop.Domain.Validates.Catalog;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductCommands
{
    public class UpdateProductCommand : BaseValidator, IRequest<Result>
    {
        public UpdateProductCommand(long productId, string name, string description, decimal price,
            string pictureFileName,
            string pictureUri, long productTypeId, long productBrandId, bool isDeleted, bool isPublished)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            Price = price;
            PictureFileName = pictureFileName;
            PictureUri = pictureUri;
            ProductTypeId = productTypeId;
            ProductBrandId = productBrandId;
            IsDeleted = isDeleted;
            IsPublished = isPublished;
        }

        public long ProductId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string PictureFileName { get; private set; }
        public string PictureUri { get; private set; }
        public long ProductTypeId { get; private set; }
        public long ProductBrandId { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsPublished { get; private set; }

        public override bool IsValid()
        {
            var validatorResult = new UpdateProductCommandValidator().Validate(this);
            return validatorResult.IsValid;
        }
    }
}