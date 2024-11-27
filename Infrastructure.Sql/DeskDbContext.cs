using System.Reflection;
using Desk.Domain.Entities;
using Desk.Domain.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Desk.Infrastructure.Sql;

public class DeskDbContext : IdentityDbContext<User, Role, Guid>, IUnitOfWork
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public DeskDbContext(DbContextOptions<DeskDbContext> options) : base(options)
    {
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public DbSet<Tag> Tags { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<TextComment> TextComments { get; set; }

    public DbSet<UserAuditEntry> UserAuditEntries { get; set; }

    public async Task CommitChangesAsync(CancellationToken ct)
    {
        _ = await SaveChangesAsync(ct);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyTimestamps()
    {
        var addedItems = ChangeTracker.Entries().Where(c => c.State == EntityState.Added);
        
        foreach (var item in addedItems)
        {
            if (item.Entity.GetType().GetProperties().Any(p => p.Name == "CreatedOn"))
            {
                var createdOnProperty = item.Entity.GetType().GetProperty("CreatedOn");
                createdOnProperty!.SetValue(item.Entity, DateTimeOffset.UtcNow);
            }
        }

        var updatedItems = ChangeTracker.Entries().Where(c => c.State == EntityState.Modified);

        foreach (var item in updatedItems)
        {
            if (item.Entity.GetType().GetProperties().Any(p => p.Name == "UpdatedOn"))
            {
                var updatedOnProperty = item.Entity.GetType().GetProperty("UpdatedOn");
                updatedOnProperty!.SetValue(item.Entity, DateTimeOffset.UtcNow);
            }
        }
    }
}