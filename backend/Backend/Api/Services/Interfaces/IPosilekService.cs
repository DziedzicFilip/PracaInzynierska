using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IPosilekService
{
    Task<IReadOnlyCollection<PosilekDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PosilekDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PosilekDto> CreateAsync(PosilekDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, PosilekDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
