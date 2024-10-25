using Desk.Domain.Entities;
using Desk.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Desk.Infrastructure.Sql.Repositories;

public class TagRepository : ITagRepository
{
    private readonly DeskDbContext _dbContext;

    public TagRepository(DeskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Tag tag, CancellationToken ct)
    {
        await _dbContext.Tags.AddAsync(tag, ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var tag = await GetByIdAsync(id, ct);

        if (tag is null)
        {
            return;
        }

        _dbContext.Tags.Remove(tag);
    }

    public async Task<List<Tag>> GetAllAsync(CancellationToken ct)
    {
        var tags = await _dbContext.Tags.Include(t => t.Owner).ToListAsync(ct);

        return tags;
    }

    public async Task<Tag?> GetByIdAsync(int id, CancellationToken ct)
    {
        var tag = await _dbContext.Tags.Include(t => t.Owner).SingleOrDefaultAsync(t => t.Id == id, ct);

        return tag; 
    }

    public async Task<Tag?> GetByUserAndIdAsync(int tagId, Guid userId, CancellationToken ct)
    {
        var tag = await _dbContext.Tags.Include(t => t.Owner).SingleOrDefaultAsync(t => t.Id == tagId && t.OwnerId == userId, ct);

        return tag;
    }

    public async Task<List<Tag>> GetByUserAsync(Guid userId, CancellationToken ct)
    {
        var tags = await _dbContext.Tags.Where(t => t.OwnerId == userId).ToListAsync(ct);

        return tags;
    }
}