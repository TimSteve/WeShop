using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeShop.Domain.Entities;

namespace WeShop.Infrasture.Data.EntityConfigurations
{
    class ProductEntityTypeConfiguration
        : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(ci => ci.Price)
                .IsRequired();

            builder.Property(ci => ci.IsDeleted)
                .IsRequired();

            builder.Property(ci => ci.PictureFileName)
                .IsRequired(false);

            builder.Ignore(ci => ci.PictureUri);

            builder.HasOne(ci => ci.ProductBrand)
                .WithMany()
                .HasForeignKey(ci => ci.ProductBrandId);

            builder.HasOne(ci => ci.ProductType)
                .WithMany()
                .HasForeignKey(ci => ci.ProductTypeId);
        }
    }
}