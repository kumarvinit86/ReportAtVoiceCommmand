using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reports.Persistence.Entities;

namespace Reports.Persistence.EntityConfigurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.OrderNumber)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(o => o.TotalAmount)
            .HasColumnType("decimal(18,2)");
    }
}
