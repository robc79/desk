using Desk.Domain.Entities;
using Desk.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desk.Infrastructure.Sql.Configurations;

public class UserAuditEntryConfiguration : IEntityTypeConfiguration<UserAuditEntry>
{
    public void Configure(EntityTypeBuilder<UserAuditEntry> builder)
    {
        builder.Property(e => e.EventType).IsRequired().HasMaxLength(Constants.MaxEventTypeLength);
        builder.HasOne(e => e.User).WithMany().OnDelete(DeleteBehavior.NoAction);
    }
}
