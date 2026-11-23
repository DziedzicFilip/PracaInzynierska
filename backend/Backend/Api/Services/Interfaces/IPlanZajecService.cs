using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IPlanZajecService
{
    Task<IReadOnlyCollection<PlanZajecDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PlanZajecDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PlanZajecDto> CreateAsync(PlanZajecDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, PlanZajecDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
