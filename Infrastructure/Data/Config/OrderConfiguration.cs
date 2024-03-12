
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Configure owned properties ( khong co table rieng )
            // Configures a relationship where the target entity is owned by (or part of) this entity.
            builder.OwnsOne(o => o.ShipToAddress, a =>
            {
                a.WithOwner();
            });

            // The entity type 'Address' is an optional dependent using table sharing without any required non shared property that could be used to identify whether the entity exists
            // Vì ShipToAddress là 1 kiểu dữ liệu mình định nghĩa nên phải dùng Navigation , không được dùng property 
            // builder.Property(p => p.ShipToAddress).IsRequired(); Sai 
            builder.Navigation(p => p.ShipToAddress).IsRequired(); // Đúng 

            // Convert the enum value HasConversion(chuyen doi kieu gi , chuyen doi cai gi  )
            builder.Property(o => o.OrderStatus).HasConversion(
                o => o.ToString(),
                o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
            );

            // Configure relationship between Order table and OrderItems table (quan he 1 nhieu )
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

        }
    }
}