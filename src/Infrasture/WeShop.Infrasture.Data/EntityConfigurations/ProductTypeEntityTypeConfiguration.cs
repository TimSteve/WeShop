using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeShop.Domain.Entities;

namespace WeShop.Infrasture.Data.EntityConfigurations
{
    class ProductTypeEntityTypeConfiguration
        : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.ToTable("ProductType");

            builder.HasKey(ci => ci.Id);

            builder.Property(cb => cb.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ci => ci.IsDeleted)
                .IsRequired();
        }
    }
}