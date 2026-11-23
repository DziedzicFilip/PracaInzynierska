using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class DzieckoAlergenService : IDzieckoAlergenService
{
    private readonly ApplicationDbContext _dbContext;

    public DzieckoAlergenService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<DzieckoAlergenDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.DzieckoAlergeny.AsNoTracking()
            .OrderBy(dzieckoAlergen => dzieckoAlergen.DzieckoId)
            .Select(dzieckoAlergen => new DzieckoAlergenDto
            {
                DzieckoId = dzieckoAlergen.DzieckoId,
                                AlergenId = dzieckoAlergen.AlergenId
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<DzieckoAlergenDto?> GetByIdAsync(int dzieckoId, int alergenId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.DzieckoAlergeny.AsNoTracking()
            .Where(dzieckoAlergen => dzieckoAlergen.DzieckoId == dzieckoId && dzieckoAlergen.AlergenId == alergenId)
            .Select(dzieckoAlergen => new DzieckoAlergenDto
            {
                DzieckoId = dzieckoAlergen.DzieckoId,
                                AlergenId = dzieckoAlergen.AlergenId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<DzieckoAlergenDto> CreateAsync(DzieckoAlergenDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new DzieckoAlergen
        {
            DzieckoId = dto.DzieckoId,
            AlergenId = dto.AlergenId,
        };
        _dbContext.DzieckoAlergeny.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int dzieckoId, int alergenId, DzieckoAlergenDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.DzieckoAlergeny.FirstOrDefaultAsync(dzieckoAlergen => dzieckoAlergen.DzieckoId == dzieckoId && dzieckoAlergen.AlergenId == alergenId, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int dzieckoId, int alergenId, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.DzieckoAlergeny.FirstOrDefaultAsync(dzieckoAlergen => dzieckoAlergen.DzieckoId == dzieckoId && dzieckoAlergen.AlergenId == alergenId, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.DzieckoAlergeny.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static DzieckoAlergenDto MapToDto(DzieckoAlergen entity)
    {
        return new DzieckoAlergenDto
        {
            DzieckoId = entity.DzieckoId,
                            AlergenId = entity.AlergenId
        };
    }
}
