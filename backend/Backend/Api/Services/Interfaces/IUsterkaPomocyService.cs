using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IUsterkaPomocyService
{
    Task<IReadOnlyCollection<UsterkaPomocyDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<UsterkaPomocyDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<UsterkaPomocyDto> CreateAsync(UsterkaPomocyDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, UsterkaPomocyDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
