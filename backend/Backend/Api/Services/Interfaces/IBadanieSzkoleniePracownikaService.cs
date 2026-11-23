using Api.Dtos;

namespace Api.Services.Interfaces;

public interface IBadanieSzkoleniePracownikaService
{
    Task<IReadOnlyCollection<BadanieSzkoleniePracownikaDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BadanieSzkoleniePracownikaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<BadanieSzkoleniePracownikaDto> CreateAsync(BadanieSzkoleniePracownikaDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, BadanieSzkoleniePracownikaDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
