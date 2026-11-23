using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IObecnoscService
{
    Task<IReadOnlyCollection<ObecnoscDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ObecnoscDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ObecnoscDto> CreateAsync(ObecnoscDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, ObecnoscDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
