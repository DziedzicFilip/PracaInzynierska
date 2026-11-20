using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IPomocDydaktycznaService
{
    Task<IReadOnlyCollection<PomocDydaktycznaDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PomocDydaktycznaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PomocDydaktycznaDto> CreateAsync(PomocDydaktycznaDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, PomocDydaktycznaDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
