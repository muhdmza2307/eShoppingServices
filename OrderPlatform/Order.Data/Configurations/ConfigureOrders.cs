using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Data.Configurations;

public class ConfigureOrders : IEntityTypeConfiguration<Entities.Order>
{
    public void Configure(EntityTypeBuilder<Entities.Order> builder)
    {
        builder.HasKey(b => b.Id).IsClustered(false);
        builder.Property(b => b.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(b => b.TotalPrice).HasPrecision(19, 4);
    }
}