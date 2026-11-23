using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IPlanDniaGrupyService
{
    Task<IReadOnlyCollection<PlanDniaGrupyDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PlanDniaGrupyDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PlanDniaGrupyDto> CreateAsync(PlanDniaGrupyDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, PlanDniaGrupyDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
