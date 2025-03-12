using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Data.Configurations;

public class ConfigureInventories : IEntityTypeConfiguration<Entities.Inventory>
{
    public void Configure(EntityTypeBuilder<Entities.Inventory> builder)
    {
        builder.HasKey(b => b.Id).IsClustered(false);
        builder.Property(b => b.Id).ValueGeneratedOnAdd().IsRequired();
    }
}