using Desk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desk.Infrastructure.Sql.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.Property(i => i.Name).IsRequired().HasMaxLength(-1);
        builder.Property(i => i.Description).IsRequired().HasMaxLength(-1);
        builder.HasOne(i => i.Owner).WithMany().OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(i => i.Tags).WithMany().UsingEntity("ItemTags");
    }
}
