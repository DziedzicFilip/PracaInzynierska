using Api.Dtos;

namespace Api.Services.Interfaces;

public interface INadgodzinyPracownikaService
{
    Task<IReadOnlyCollection<NadgodzinyPracownikaDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<NadgodzinyPracownikaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<NadgodzinyPracownikaDto> CreateAsync(NadgodzinyPracownikaDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, NadgodzinyPracownikaDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
