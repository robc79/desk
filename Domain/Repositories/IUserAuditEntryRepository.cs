using Desk.Domain.Entities;

namespace Desk.Domain.Repositories;

public interface IUserAuditEntryRepository
{
    Task AddAsync(UserAuditEntry entry, CancellationToken ct);
}