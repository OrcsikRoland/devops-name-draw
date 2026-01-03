using Microsoft.EntityFrameworkCore;
using NameDraw.Api.Data;
using NameDraw.Api.Entities;

namespace NameDraw.Api.Repositories
{
    public class EfNameRepository : INameRepository
    {
        private readonly AppDbContext _db;

        public EfNameRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task<List<NameItem>> GetAllAsync(string sessionId) =>
    _db.Names.Where(x => x.SessionId == sessionId)
             .OrderByDescending(x => x.Id)
             .ToListAsync();

        public async Task AddAsync(string sessionId, string value)
        {
            _db.Names.Add(new NameItem { SessionId = sessionId, Value = value, CreatedAt = DateTime.UtcNow });
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(string sessionId, int id)
        {
            var e = await _db.Names.FirstOrDefaultAsync(x => x.SessionId == sessionId && x.Id == id);
            if (e == null) return;
            _db.Names.Remove(e);
            await _db.SaveChangesAsync();
        }

        public async Task<NameItem?> GetRandomAsync(string sessionId)
        {
            var q = _db.Names.Where(x => x.SessionId == sessionId);
            var count = await q.CountAsync();
            if (count == 0) return null;

            var skip = Random.Shared.Next(count);
            return await q.OrderBy(x => x.Id).Skip(skip).FirstOrDefaultAsync();
        }

        public async Task ClearAsync(string sessionId)
        {
            var rows = _db.Names.Where(x => x.SessionId == sessionId);
            _db.Names.RemoveRange(rows);
            await _db.SaveChangesAsync();
        }
    }
}
