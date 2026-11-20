using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IRodzicDzieckoService
{
    Task<IReadOnlyCollection<RodzicDzieckoDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RodzicDzieckoDto?> GetByIdAsync(int rodzicId, int dzieckoId, CancellationToken cancellationToken = default);
    Task<RodzicDzieckoDto> CreateAsync(RodzicDzieckoDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int rodzicId, int dzieckoId, RodzicDzieckoDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int rodzicId, int dzieckoId, CancellationToken cancellationToken = default);
}
