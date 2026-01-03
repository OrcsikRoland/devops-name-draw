using NameDraw.Api.Entities;

namespace NameDraw.Api.Repositories
{
    public interface INameRepository
    {
        Task<List<NameItem>> GetAllAsync(string sessionId);
        Task AddAsync(string sessionId, string value);
        Task DeleteAsync(string sessionId, int id);
        Task<NameItem?> GetRandomAsync(string sessionId);
        Task ClearAsync(string sessionId);

    }
}
