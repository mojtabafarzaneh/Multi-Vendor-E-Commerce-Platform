using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multi_VendorE_CommercePlatform.Models.ValueGenerator;

namespace Multi_VendorE_CommercePlatform.Models.Configuration;

public class ProductConfigurations: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        
        builder
            .Property(p => p.Price)
            .HasColumnType("decimal(18, 2)");
        
        builder
            .HasOne(p => p.Vendor)
            .WithMany(v => v.Products)
            .HasForeignKey(p => p.VendorId);
        
        builder
            .Property<Guid>("Id")
            .HasColumnType("uniqueidentifier")
            .HasValueGenerator<GuidValueGenerator>();
        builder
            .Property<DateTime>("CreatedAt")
            .HasColumnType("datetime")
            .HasValueGenerator<CreatedAtValueGenerator>();
        builder
            .Property<DateTime>("UpdatedAt")
            .HasColumnType("datetime")
            .HasValueGenerator<CreatedAtValueGenerator>();
    }
}