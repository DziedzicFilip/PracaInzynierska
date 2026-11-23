using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class DzieckoDietaService : IDzieckoDietaService
{
    private readonly ApplicationDbContext _dbContext;

    public DzieckoDietaService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<DzieckoDietaDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.DzieckoDiety.AsNoTracking()
            .OrderBy(dzieckoDieta => dzieckoDieta.DzieckoId)
            .Select(dzieckoDieta => new DzieckoDietaDto
            {
                DzieckoId = dzieckoDieta.DzieckoId,
                                DietaId = dzieckoDieta.DietaId
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<DzieckoDietaDto?> GetByIdAsync(int dzieckoId, int dietaId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.DzieckoDiety.AsNoTracking()
            .Where(dzieckoDieta => dzieckoDieta.DzieckoId == dzieckoId && dzieckoDieta.DietaId == dietaId)
            .Select(dzieckoDieta => new DzieckoDietaDto
            {
                DzieckoId = dzieckoDieta.DzieckoId,
                                DietaId = dzieckoDieta.DietaId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<DzieckoDietaDto> CreateAsync(DzieckoDietaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new DzieckoDieta
        {
            DzieckoId = dto.DzieckoId,
            DietaId = dto.DietaId,
        };
        _dbContext.DzieckoDiety.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int dzieckoId, int dietaId, DzieckoDietaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.DzieckoDiety.FirstOrDefaultAsync(dzieckoDieta => dzieckoDieta.DzieckoId == dzieckoId && dzieckoDieta.DietaId == dietaId, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int dzieckoId, int dietaId, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.DzieckoDiety.FirstOrDefaultAsync(dzieckoDieta => dzieckoDieta.DzieckoId == dzieckoId && dzieckoDieta.DietaId == dietaId, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.DzieckoDiety.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static DzieckoDietaDto MapToDto(DzieckoDieta entity)
    {
        return new DzieckoDietaDto
        {
            DzieckoId = entity.DzieckoId,
                            DietaId = entity.DietaId
        };
    }
}
