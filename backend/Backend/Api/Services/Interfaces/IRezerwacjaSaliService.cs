using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IRezerwacjaSaliService
{
    Task<IReadOnlyCollection<RezerwacjaSaliDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RezerwacjaSaliDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<RezerwacjaSaliDto> CreateAsync(RezerwacjaSaliDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, RezerwacjaSaliDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
