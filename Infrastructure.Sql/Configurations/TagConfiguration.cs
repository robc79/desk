using Desk.Domain.Entities;
using Desk.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desk.Infrastructure.Sql.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(t => t.Name).IsRequired().HasMaxLength(Constants.MaxTagLength);
        builder.HasOne(t => t.Owner).WithMany().OnDelete(DeleteBehavior.NoAction);
    }
}