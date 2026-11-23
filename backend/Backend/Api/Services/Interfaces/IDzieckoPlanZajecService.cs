using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IDzieckoPlanZajecService
{
    Task<IReadOnlyCollection<DzieckoPlanZajecDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DzieckoPlanZajecDto?> GetByIdAsync(int dzieckoId, int planZajecId, CancellationToken cancellationToken = default);
    Task<DzieckoPlanZajecDto> CreateAsync(DzieckoPlanZajecDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int dzieckoId, int planZajecId, DzieckoPlanZajecDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int dzieckoId, int planZajecId, CancellationToken cancellationToken = default);
}
