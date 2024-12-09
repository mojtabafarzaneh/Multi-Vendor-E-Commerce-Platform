﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Multi_VendorE_CommercePlatform.Models.ValueGenerator;

public class GuidValueGenerator : ValueGenerator<Guid>
{
    public override Guid Next(EntityEntry entry)
    {
        return Guid.NewGuid();
    }

    public override bool GeneratesTemporaryValues => false;
}