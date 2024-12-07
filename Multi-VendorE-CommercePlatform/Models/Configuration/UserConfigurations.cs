﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multi_VendorE_CommercePlatform.Models.ValueGenerator;

namespace Multi_VendorE_CommercePlatform.Models.Configuration;

public class UserConfigurations: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
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