using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IWydarzenieKalendarzService
{
    Task<IReadOnlyCollection<WydarzenieKalendarzDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<WydarzenieKalendarzDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<WydarzenieKalendarzDto> CreateAsync(WydarzenieKalendarzDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, WydarzenieKalendarzDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
