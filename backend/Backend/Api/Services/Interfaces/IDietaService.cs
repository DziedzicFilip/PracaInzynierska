using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IDietaService
{
    Task<IReadOnlyCollection<DietaDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DietaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<DietaDto> CreateAsync(DietaDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, DietaDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
