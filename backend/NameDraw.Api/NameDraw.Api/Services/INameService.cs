using NameDraw.Api.Dtos;

namespace NameDraw.Api.Services
{
    public interface INameService
    {
        Task<List<NameDto>> GetAllAsync(string sessionId);
        Task AddAsync(string sessionId, string value);
        Task DeleteAsync(string sessionId, int id);
        Task<NameDto?> GetRandomAsync(string sessionId);
        Task ClearAsync(string sessionId);
    }
}
