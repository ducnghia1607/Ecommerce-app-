using System.Reflection;
using Core;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }

    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            // Vòng lặp qua các entity 
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // lấy ra các property có kiểu là decimal 
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                // lặp qua từng property có kiểu là decimal để chuyển nó sang kiểu double 
                foreach (var property in properties)
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                }
            }
        }
    }
}
