using NameDraw.Api.Dtos;
using NameDraw.Api.Repositories;

namespace NameDraw.Api.Services
{
    public class NameService : INameService
    {
        private readonly INameRepository _repo;

        public NameService(INameRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<NameDto>> GetAllAsync(string sessionId)
        {
            var items = await _repo.GetAllAsync(sessionId);
            return items.Select(x => new NameDto
            {
                Id = x.Id,
                Value = x.Value,
                CreatedAt = x.CreatedAt
            }).ToList();
        }

        public async Task AddAsync(string sessionId, string value)
        {
            var v = value?.Trim();
            if (string.IsNullOrWhiteSpace(v))
                throw new ArgumentException("A név nem lehet üres.");
            if (v.Length > 50)
                throw new ArgumentException("Max 50 karakter lehet.");

            await _repo.AddAsync(sessionId, v);
        }

        public Task DeleteAsync(string sessionId, int id)
            => _repo.DeleteAsync(sessionId, id);

        public async Task<NameDto?> GetRandomAsync(string sessionId)
        {
            var item = await _repo.GetRandomAsync(sessionId);
            if (item is null) return null;

            return new NameDto
            {
                Id = item.Id,
                Value = item.Value,
                CreatedAt = item.CreatedAt
            };
        }

        public Task ClearAsync(string sessionId)
            => _repo.ClearAsync(sessionId);
    }
}
