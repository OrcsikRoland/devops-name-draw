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

        public Task<List<NameItem>> GetAllAsync() =>
            _db.Names
               .OrderByDescending(x => x.Id)
               .ToListAsync();

        public async Task AddAsync(string value)
        {
            _db.Names.Add(new NameItem
            {
                
                Value = value,
                CreatedAt = DateTime.UtcNow
            });

            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var e = await _db.Names.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null) return;

            _db.Names.Remove(e);
            await _db.SaveChangesAsync();
        }

        public async Task<NameItem?> GetRandomAsync()
        {
            var q = _db.Names;
            var count = await q.CountAsync();
            if (count == 0) return null;

            var skip = Random.Shared.Next(count);
            return await q.OrderBy(x => x.Id)
                          .Skip(skip)
                          .FirstOrDefaultAsync();
        }

        public async Task ClearAsync()
        {
            var rows = await _db.Names.ToListAsync();
            if (rows.Count == 0) return;

            _db.Names.RemoveRange(rows);
            await _db.SaveChangesAsync();
        }
    }
}
