using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PosilekAlergenService : IPosilekAlergenService
{
    private readonly ApplicationDbContext _dbContext;

    public PosilekAlergenService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<PosilekAlergenDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.PosilekAlergeny.AsNoTracking()
            .OrderBy(posilekAlergen => posilekAlergen.PosilekId)
            .Select(posilekAlergen => new PosilekAlergenDto
            {
                PosilekId = posilekAlergen.PosilekId,
                                AlergenId = posilekAlergen.AlergenId
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<PosilekAlergenDto?> GetByIdAsync(int posilekId, int alergenId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.PosilekAlergeny.AsNoTracking()
            .Where(posilekAlergen => posilekAlergen.PosilekId == posilekId && posilekAlergen.AlergenId == alergenId)
            .Select(posilekAlergen => new PosilekAlergenDto
            {
                PosilekId = posilekAlergen.PosilekId,
                                AlergenId = posilekAlergen.AlergenId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PosilekAlergenDto> CreateAsync(PosilekAlergenDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new PosilekAlergen
        {
            PosilekId = dto.PosilekId,
            AlergenId = dto.AlergenId,
        };
        _dbContext.PosilekAlergeny.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int posilekId, int alergenId, PosilekAlergenDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.PosilekAlergeny.FirstOrDefaultAsync(posilekAlergen => posilekAlergen.PosilekId == posilekId && posilekAlergen.AlergenId == alergenId, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int posilekId, int alergenId, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.PosilekAlergeny.FirstOrDefaultAsync(posilekAlergen => posilekAlergen.PosilekId == posilekId && posilekAlergen.AlergenId == alergenId, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.PosilekAlergeny.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static PosilekAlergenDto MapToDto(PosilekAlergen entity)
    {
        return new PosilekAlergenDto
        {
            PosilekId = entity.PosilekId,
                            AlergenId = entity.AlergenId
        };
    }
}
