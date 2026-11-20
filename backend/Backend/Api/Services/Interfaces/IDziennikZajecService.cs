using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IDziennikZajecService
{
    Task<IReadOnlyCollection<DziennikZajecDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DziennikZajecDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<DziennikZajecDto> CreateAsync(DziennikZajecDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, DziennikZajecDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
