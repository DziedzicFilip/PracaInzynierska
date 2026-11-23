using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IPosilekAlergenService
{
    Task<IReadOnlyCollection<PosilekAlergenDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PosilekAlergenDto?> GetByIdAsync(int posilekId, int alergenId, CancellationToken cancellationToken = default);
    Task<PosilekAlergenDto> CreateAsync(PosilekAlergenDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int posilekId, int alergenId, PosilekAlergenDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int posilekId, int alergenId, CancellationToken cancellationToken = default);
}
