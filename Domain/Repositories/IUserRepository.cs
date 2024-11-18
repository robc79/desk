using Desk.Domain.Entities;

namespace Desk.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken ct);

    Task<List<User>> GetAllAsync(CancellationToken ct);
}