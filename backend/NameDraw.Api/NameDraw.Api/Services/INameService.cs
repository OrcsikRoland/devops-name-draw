using NameDraw.Api.Dtos;

namespace NameDraw.Api.Services
{
    public interface INameService
    {
        Task<List<NameDto>> GetAllAsync();
        Task AddAsync(string value);
        Task DeleteAsync(int id);
        Task<NameDto?> GetRandomAsync();
    }
}
