using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IAlergenService
{
    Task<IReadOnlyCollection<AlergenDto>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<AlergenDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<AlergenDto> CreateAsync(
        AlergenDto alergen,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(
        int id,
        AlergenDto alergen,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}
