using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IOsobaUpowaznionaOdbiorService
{
    Task<IReadOnlyCollection<OsobaUpowaznionaOdbiorDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OsobaUpowaznionaOdbiorDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<OsobaUpowaznionaOdbiorDto> CreateAsync(OsobaUpowaznionaOdbiorDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, OsobaUpowaznionaOdbiorDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
