namespace WeShop.Queries.Dtos
{
    public class ProductDto
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUri { get; set; }
        public CategoryDto ProductType { get; set; }
        public BrandDto ProductBrand { get; set; }
    }
}