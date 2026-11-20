using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IDzieckoService
{
    Task<IReadOnlyCollection<DzieckoDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DzieckoDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<DzieckoDto> CreateAsync(DzieckoDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, DzieckoDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
