using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IRodzicService
{
    Task<IReadOnlyCollection<RodzicDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RodzicDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<RodzicDto> CreateAsync(RodzicDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, RodzicDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
