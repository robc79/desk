namespace Desk.Domain.Repositories;

public interface IUnitOfWork
{
    Task CommitChangesAsync(CancellationToken ct);
}