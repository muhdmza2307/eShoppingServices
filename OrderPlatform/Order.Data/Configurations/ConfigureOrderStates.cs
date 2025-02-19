using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Common.Enums;
using Order.Common.Extensions;
using Order.Data.Entities;

namespace Order.Data.Configurations;

public class ConfigureOrderStates : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Description).IsRequired();
        var entries = Enum.GetValues(typeof(OrderState))
            .Cast<OrderState>()
            .Select(e => new OrderStatus { Id = e, Description = e.GetEnumDescription() })
            .ToArray();
        builder.HasData(entries);
    }
}