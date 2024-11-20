using Desk.Domain.Entities;
using Desk.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Desk.Infrastructure.Sql.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DeskDbContext _dbContext;

    public UserRepository(DeskDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

        return user;
    }
}
