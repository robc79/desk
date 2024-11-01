using Desk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desk.Infrastructure.Sql.Configurations;

public class TextCommentConfiguration : IEntityTypeConfiguration<TextComment>
{
    public void Configure(EntityTypeBuilder<TextComment> builder)
    {
        builder.Property(c => c.Comment).IsRequired().HasMaxLength(-1);
        builder.HasOne(c => c.Item).WithMany(i => i.TextComments);
    }
}
