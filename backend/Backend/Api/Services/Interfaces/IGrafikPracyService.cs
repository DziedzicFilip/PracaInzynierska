using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IGrafikPracyService
{
    Task<IReadOnlyCollection<GrafikPracyDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<GrafikPracyDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<GrafikPracyDto> CreateAsync(GrafikPracyDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, GrafikPracyDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
