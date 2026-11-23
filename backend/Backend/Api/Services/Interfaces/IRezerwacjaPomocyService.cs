using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IRezerwacjaPomocyService
{
    Task<IReadOnlyCollection<RezerwacjaPomocyDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RezerwacjaPomocyDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<RezerwacjaPomocyDto> CreateAsync(RezerwacjaPomocyDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, RezerwacjaPomocyDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
