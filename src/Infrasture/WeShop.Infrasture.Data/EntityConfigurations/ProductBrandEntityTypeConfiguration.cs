using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeShop.Domain.Entities;

namespace WeShop.Infrasture.Data.EntityConfigurations
{
    class ProductBrandEntityTypeConfiguration
        : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.ToTable("ProductBrand");

            builder.HasKey(ci => ci.Id);

            builder.Property(cb => cb.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(cb => cb.IsDeleted)
                .IsRequired();
        }
    }
}