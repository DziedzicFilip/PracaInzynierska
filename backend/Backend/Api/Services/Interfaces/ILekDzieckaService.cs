using Api.Dtos;

namespace Api.Services.Interfaces;

public interface ILekDzieckaService
{
    Task<IReadOnlyCollection<LekDzieckaDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<LekDzieckaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<LekDzieckaDto> CreateAsync(LekDzieckaDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, LekDzieckaDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
