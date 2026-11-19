using Api.Dtos;

namespace Api.Services.Interfaces;

public interface ISalaService
{
    Task<IReadOnlyCollection<SalaDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SalaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<SalaDto> CreateAsync(SalaDto sala, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, SalaDto sala, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
