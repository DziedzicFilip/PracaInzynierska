using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IIncydentService
{
    Task<IReadOnlyCollection<IncydentDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IncydentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IncydentDto> CreateAsync(IncydentDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, IncydentDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
