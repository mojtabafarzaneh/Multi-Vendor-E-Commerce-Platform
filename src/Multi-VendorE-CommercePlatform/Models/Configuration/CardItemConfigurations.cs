using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multi_VendorE_CommercePlatform.Models.ValueGenerator;

namespace Multi_VendorE_CommercePlatform.Models.Configuration;

public class CardItemConfigurations: IEntityTypeConfiguration<CardItem>
{
    public void Configure(EntityTypeBuilder<CardItem> builder)
    {
        builder
            .Property(m => m.Price)
            .HasColumnType("decimal(18, 2)");
        builder
            .HasOne(ci => ci.Card)
            .WithMany(c => c.CardItems)
            .HasForeignKey(ci => ci.CardId);

        builder
            .HasOne(ci => ci.Product)
            .WithMany(p => p.CardItems)
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .Property<Guid>("Id")
            .HasColumnType("uniqueidentifier")
            .HasValueGenerator<GuidValueGenerator>();
    }
}