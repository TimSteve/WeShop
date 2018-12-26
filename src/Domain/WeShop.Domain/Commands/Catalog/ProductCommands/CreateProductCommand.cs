using MediatR;
using WeShop.Domain.Abstract;
using WeShop.Domain.Validates.Catalog;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Commands.Catalog.ProductCommands
{
    public class CreateProductCommand :BaseValidator, IRequest<Result<string>>
    {
        public CreateProductCommand(
            string name,
            string description,
            decimal price,
            string pictureFileName,
            string pictureUri,
            long productBrandId,
            long productTypeId,
            bool isPublished)
        {
            Name = name;
            Description = description;
            Price = price;
            PictureFileName = pictureFileName;
            PictureUri = pictureUri;
            ProductBrandId = productBrandId;
            ProductTypeId = productTypeId;
            IsPublished = isPublished;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string PictureFileName { get; private set; }
        public string PictureUri { get; private set; }
        public long ProductTypeId { get; private set; }
        public long ProductBrandId { get; private set; }
        public bool IsPublished { get; private set; }
        
        public override bool IsValid()
        {
            var validatorResult = new CreateProductCommandValidator().Validate(this);
            return validatorResult.IsValid;
        }
    }
}