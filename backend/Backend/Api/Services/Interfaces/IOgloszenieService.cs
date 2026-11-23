using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IOgloszenieService
{
    Task<IReadOnlyCollection<OgloszenieDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OgloszenieDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<OgloszenieDto> CreateAsync(OgloszenieDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, OgloszenieDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
