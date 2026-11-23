using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IRejestrPodaniaLekowService
{
    Task<IReadOnlyCollection<RejestrPodaniaLekowDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RejestrPodaniaLekowDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<RejestrPodaniaLekowDto> CreateAsync(RejestrPodaniaLekowDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, RejestrPodaniaLekowDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
