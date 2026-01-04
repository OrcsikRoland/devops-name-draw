using NameDraw.Api.Entities;

namespace NameDraw.Api.Repositories
{
    public interface INameRepository
    {
        Task<List<NameItem>> GetAllAsync();
        Task AddAsync(string value);
        Task DeleteAsync( int id);
        Task<NameItem?> GetRandomAsync();
        Task ClearAsync();
    }
}
