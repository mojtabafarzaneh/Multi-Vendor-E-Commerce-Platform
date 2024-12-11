using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Multi_VendorE_CommercePlatform.Models.Configuration;

namespace Multi_VendorE_CommercePlatform.Models.Entities;

public class ApplicationDbContext : IdentityDbContext<User, UserRoles, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public override DbSet<User> Users => Set<User>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<Card> Cards => Set<Card>();
    public DbSet<CardItem> CardItems => Set<CardItem>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserRoleConfigurations());
        builder.ApplyConfiguration(new UserConfigurations());
        builder.ApplyConfiguration(new ProductConfigurations());
        builder.ApplyConfiguration(new CustomerConfigurations());
        builder.ApplyConfiguration(new OrderConfigurations());
        builder.ApplyConfiguration(new OrderItemConfigurations());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new CardConfigurations());
        builder.ApplyConfiguration(new CardItemConfigurations());
        builder.ApplyConfiguration(new VendorConfigurations());
    }
}