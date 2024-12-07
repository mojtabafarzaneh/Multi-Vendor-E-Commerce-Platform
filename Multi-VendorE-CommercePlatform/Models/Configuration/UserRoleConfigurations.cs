using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multi_VendorE_CommercePlatform.Models.ValueGenerator;

namespace Multi_VendorE_CommercePlatform.Models.Configuration;

public class UserRoleConfigurations: IEntityTypeConfiguration<UserRoles>
{
    public void Configure(EntityTypeBuilder<UserRoles> builder)
    {
        builder
            .Property<Guid>("Id")
            .HasColumnType("uniqueidentifier")
            .HasValueGenerator<GuidValueGenerator>();
     
        builder.HasData(
            new UserRoles
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new UserRoles
            {
                Name = "Customer",
                NormalizedName = "CUSTOMER"
                
            },
            new UserRoles
            {
                Name = "Vendor",
                NormalizedName = "VENDOR"
            });
    }
}