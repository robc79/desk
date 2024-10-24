using Desk.Domain.Entities;
using Desk.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Desk.Infrastructure.Sql;

public class DeskDbContext : DbContext, IUnitOfWork
{
    public DeskDbContext(DbContextOptions<DeskDbContext> options) : base(options)
    {
    }

    public DbSet<Tag> Tags { get; set; }

    public async Task CommitChangesAsync(CancellationToken ct)
    {
        _ = await SaveChangesAsync(ct);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}