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

        public async Task<List<NameItem>> GetAllAsync()
        {
            return await _db.Names
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

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
            var entity = await _db.Names.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null) return;

            _db.Names.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<NameItem?> GetRandomAsync()
        {
            var count = await _db.Names.CountAsync();
            if (count == 0) return null;

            var skip = Random.Shared.Next(count);
            return await _db.Names
                .OrderBy(x => x.Id)
                .Skip(skip)
                .FirstOrDefaultAsync();
        }
    }
}
