using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IPrzeciwskazanieMedyczneService
{
    Task<IReadOnlyCollection<PrzeciwskazanieMedyczneDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PrzeciwskazanieMedyczneDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PrzeciwskazanieMedyczneDto> CreateAsync(PrzeciwskazanieMedyczneDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, PrzeciwskazanieMedyczneDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
