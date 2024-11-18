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

    public async Task<int> CountUserItemsAsync(Guid userId, bool withImage, CancellationToken ct)
    {
        var q = _dbContext.Items.Where(i => i.OwnerId == userId);

        if (withImage)
        {
            q = q.Where(i => i.ImageName != null);
        }

        return await q.CountAsync();
    }

    public async Task DeleteAsync(int itemId, CancellationToken ct)
    {
        var item = await _dbContext.Items.SingleOrDefaultAsync(i => i.Id == itemId, ct);

        if (item is null)
        {
            return;
        }

        _dbContext.Items.Remove(item);
    }

    public async Task<Item?> GetByUserAndIdAsync(int itemId, Guid userId, CancellationToken ct)
    {
        var item = await _dbContext
        .Items
        .Include(i => i.Tags)
        .SingleOrDefaultAsync(i => i.Id == itemId && i.OwnerId == userId);

        return item;
    }

    public async Task<List<Item>> GetByUserAndLocationAsync(ItemLocation location, Guid userId, CancellationToken ct)
    {
        var items = await _dbContext
            .Items
            .Include(i => i.Tags)
            .Where(i => i.Location == location && i.OwnerId == userId)
            .ToListAsync(ct);

        return items;
    }

    public async Task<List<Item>> GetByUserAndTagAsync(int tagId, Guid userId, CancellationToken ct)
    {
        var items = await _dbContext
            .Items
            .Include(i => i.Tags)
            .Where(i => i.Tags.Any(t => t.Id == tagId))
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
            .SingleOrDefaultAsync(i => i.Id == itemId && i.OwnerId == userId);

        return item;
    }
}
