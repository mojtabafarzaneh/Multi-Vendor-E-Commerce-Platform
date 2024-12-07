using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multi_VendorE_CommercePlatform.Models.ValueGenerator;

namespace Multi_VendorE_CommercePlatform.Models.Configuration;

public class OrderConfigurations: IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);
        
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