using Desk.Domain.Entities;

namespace Desk.Domain.Repositories;

public interface ITagRepository
{
    Task<Tag?> GetByIdAsync(int id, CancellationToken ct);

    Task<List<Tag>> GetByUserAsync(Guid userId, CancellationToken ct);

    Task<Tag?> GetByUserAndIdAsync(int tagId, Guid userId, CancellationToken ct);
    
    Task<List<Tag>> GetAllAsync(CancellationToken ct);

    Task AddAsync(Tag tag, CancellationToken ct);

    Task DeleteAsync(int id, CancellationToken ct);
}