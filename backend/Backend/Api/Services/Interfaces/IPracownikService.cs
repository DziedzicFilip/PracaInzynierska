using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IPracownikService
{
    Task<IReadOnlyCollection<PracownikDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PracownikDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PracownikDto> CreateAsync(PracownikDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, PracownikDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
