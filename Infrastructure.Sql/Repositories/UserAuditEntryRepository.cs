using Desk.Domain.Entities;
using Desk.Domain.Repositories;

namespace Desk.Infrastructure.Sql.Repositories;

public class UserAuditEntryRepository : IUserAuditEntryRepository
{
    private readonly DeskDbContext _context;

    public UserAuditEntryRepository(DeskDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddAsync(UserAuditEntry entry, CancellationToken ct)
    {
        await _context.UserAuditEntries.AddAsync(entry, ct);
    }
}
