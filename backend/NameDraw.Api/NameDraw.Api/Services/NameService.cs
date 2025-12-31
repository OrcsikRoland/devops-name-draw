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

        public async Task<List<NameDto>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.Select(x => new NameDto
            {
                Id = x.Id,
                Value = x.Value,
                CreatedAt = x.CreatedAt
            }).ToList();
        }

        public async Task AddAsync(string value)
        {
            var v = value?.Trim();
            if (string.IsNullOrWhiteSpace(v))
                throw new ArgumentException("A név nem lehet üres.");
            if (v.Length > 50)
                throw new ArgumentException("Max 50 karakter lehet.");

            await _repo.AddAsync(v);
        }

        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);

        public async Task<NameDto?> GetRandomAsync()
        {
            var item = await _repo.GetRandomAsync();
            return item == null
                ? null
                : new NameDto
                {
                    Id = item.Id,
                    Value = item.Value,
                    CreatedAt = item.CreatedAt
                };
        }
    }
}
