using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IKartaObserwacjiPedagogicznejService
{
    Task<IReadOnlyCollection<KartaObserwacjiPedagogicznejDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<KartaObserwacjiPedagogicznejDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<KartaObserwacjiPedagogicznejDto> CreateAsync(KartaObserwacjiPedagogicznejDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, KartaObserwacjiPedagogicznejDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
