using Desk.Domain.Entities;

namespace Desk.Domain.Repositories;

public interface IItemRepository
{
    Task<Item?> GetByUserAndIdAsync(int itemId, Guid userId, CancellationToken ct);

    Task AddAsync(Item item, CancellationToken ct);

    Task<List<Item>> GetByUserAndLocationAsync(ItemLocation location, Guid userId, CancellationToken ct);

    Task<Item?> GetWithCommentsByUserAndIdAsync(int itemId, Guid userId, CancellationToken ct);

    Task<List<Item>> GetByUserAndTagAsync(int tagId, Guid userId, CancellationToken ct);
}