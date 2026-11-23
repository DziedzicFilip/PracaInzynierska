using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IDzieckoAlergenService
{
    Task<IReadOnlyCollection<DzieckoAlergenDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DzieckoAlergenDto?> GetByIdAsync(int dzieckoId, int alergenId, CancellationToken cancellationToken = default);
    Task<DzieckoAlergenDto> CreateAsync(DzieckoAlergenDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int dzieckoId, int alergenId, DzieckoAlergenDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int dzieckoId, int alergenId, CancellationToken cancellationToken = default);
}
