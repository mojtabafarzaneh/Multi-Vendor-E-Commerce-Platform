using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Multi_VendorE_CommercePlatform.Models.ValueGenerator;


public class CreatedAtValueGenerator: ValueGenerator<DateTime>
{
    public override DateTime Next(EntityEntry entry)
    { 
        return DateTime.UtcNow;
    }

    public override bool GeneratesTemporaryValues => false;
}
