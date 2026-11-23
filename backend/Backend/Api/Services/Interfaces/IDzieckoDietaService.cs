using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IDzieckoDietaService
{
    Task<IReadOnlyCollection<DzieckoDietaDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DzieckoDietaDto?> GetByIdAsync(int dzieckoId, int dietaId, CancellationToken cancellationToken = default);
    Task<DzieckoDietaDto> CreateAsync(DzieckoDietaDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int dzieckoId, int dietaId, DzieckoDietaDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int dzieckoId, int dietaId, CancellationToken cancellationToken = default);
}
