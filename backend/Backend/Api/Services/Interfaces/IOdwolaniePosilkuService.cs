using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IOdwolaniePosilkuService
{
    Task<IReadOnlyCollection<OdwolaniePosilkuDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OdwolaniePosilkuDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<OdwolaniePosilkuDto> CreateAsync(OdwolaniePosilkuDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, OdwolaniePosilkuDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
