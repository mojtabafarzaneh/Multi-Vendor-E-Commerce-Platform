using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multi_VendorE_CommercePlatform.Models.ValueGenerator;

namespace Multi_VendorE_CommercePlatform.Models.Configuration;

public class CustomerConfigurations: IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .Property<Guid>("Id")
            .HasColumnType("uniqueidentifier")
            .HasValueGenerator<GuidValueGenerator>();
    }
}