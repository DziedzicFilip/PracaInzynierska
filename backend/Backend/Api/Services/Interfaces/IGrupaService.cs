using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IGrupaService
{
    Task<IReadOnlyCollection<GrupaDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<GrupaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<GrupaDto> CreateAsync(GrupaDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, GrupaDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
