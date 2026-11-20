using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IZajecieService
{
    Task<IReadOnlyCollection<ZajecieDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ZajecieDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ZajecieDto> CreateAsync(ZajecieDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, ZajecieDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
