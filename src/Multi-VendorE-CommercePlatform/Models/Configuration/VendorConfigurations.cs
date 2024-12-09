using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multi_VendorE_CommercePlatform.Models.ValueGenerator;

namespace Multi_VendorE_CommercePlatform.Models.Configuration;

public class VendorConfigurations: IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        
        builder
            .Property<Guid>("Id")
            .HasColumnType("uniqueidentifier")
            .HasValueGenerator<GuidValueGenerator>();

    }
}