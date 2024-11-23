using Desk.Domain.Entities;
using Desk.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Desk.Infrastructure.Sql.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly DeskDbContext _dbContext;

    public ItemRepository(DeskDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task AddAsync(Item item, CancellationToken ct)
    {
        await _dbContext.Items.AddAsync(item, ct);
    }

    public async Task<Item?> GetByUserAndIdAsync(int itemId, Guid userId, CancellationToken ct)
    {
        var item = await _dbContext
        .Items
        .Include(i => i.Tags)
        .SingleOrDefaultAsync(i => i.Id == itemId && i.OwnerId == userId && !i.IsDeleted);

        return item;
    }

    public async Task<List<Item>> GetByUserAndLocationAsync(ItemLocation location, Guid userId, CancellationToken ct)
    {
        var items = await _dbContext
            .Items
            .Include(i => i.Tags)
            .Where(i => i.Location == location && i.OwnerId == userId && !i.IsDeleted)
            .ToListAsync(ct);

        return items;
    }

    public async Task<List<Item>> GetByUserAndTagAsync(int tagId, Guid userId, CancellationToken ct)
    {
        var items = await _dbContext
            .Items
            .Include(i => i.Tags)
            .Where(i => i.Tags.Any(t => t.Id == tagId && i.OwnerId == userId && !i.IsDeleted))
            .ToListAsync(ct);

        return items;
    }

    public async Task<Item?> GetWithCommentsByUserAndIdAsync(int itemId, Guid userId, CancellationToken ct)
    {
        var item = await _dbContext
            .Items
            .Include(i => i.Tags)
            .Include(i => i.TextComments)
            .AsSplitQuery()
            .SingleOrDefaultAsync(i => i.Id == itemId && i.OwnerId == userId && !i.IsDeleted);

        return item;
    }
}
