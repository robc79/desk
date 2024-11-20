using Desk.Domain.Entities;

namespace Desk.Domain.Repositories;

public interface IReportRepository
{
    Task<List<UserReport>> GetUserReportsAsync(CancellationToken ct);
}