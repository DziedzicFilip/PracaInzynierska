using Api.Dtos;

namespace Api.Services.Interfaces;

public interface INieobecnoscPracownikaService
{
    Task<IReadOnlyCollection<NieobecnoscPracownikaDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<NieobecnoscPracownikaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<NieobecnoscPracownikaDto> CreateAsync(NieobecnoscPracownikaDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, NieobecnoscPracownikaDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
