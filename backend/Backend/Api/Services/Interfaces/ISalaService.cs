using Api.Models;

namespace Api.Services.Interfaces;

public interface ISalaService
{
    Task<IReadOnlyCollection<Sala>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Sala?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Sala> CreateAsync(Sala sala, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, Sala sala, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
