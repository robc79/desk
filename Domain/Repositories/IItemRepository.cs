using Desk.Domain.Entities;

namespace Desk.Domain.Repositories;

public interface IItemRepository
{
    Task<Item?> GetByUserAndIdAsync(int itemId, Guid userId, CancellationToken ct);

    Task AddAsync(Item item, CancellationToken ct);

    Task DeleteAsync(int itemId, CancellationToken ct);

    Task<List<Item>> GetByUserAndLocation(ItemLocation location, Guid userId, CancellationToken ct);
}