using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class OdwolaniePosilkuService : IOdwolaniePosilkuService
{
    private readonly ApplicationDbContext _dbContext;

    public OdwolaniePosilkuService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<OdwolaniePosilkuDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.OdwolaniaPosilkow.AsNoTracking()
            .OrderBy(odwolaniePosilku => odwolaniePosilku.Id)
            .Select(odwolaniePosilku => new OdwolaniePosilkuDto
            {
                Id = odwolaniePosilku.Id,
                                DzieckoId = odwolaniePosilku.DzieckoId,
                                DataPosilku = odwolaniePosilku.DataPosilku,
                                DataZgloszenia = odwolaniePosilku.DataZgloszenia
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<OdwolaniePosilkuDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.OdwolaniaPosilkow.AsNoTracking()
            .Where(odwolaniePosilku => odwolaniePosilku.Id == id)
            .Select(odwolaniePosilku => new OdwolaniePosilkuDto
            {
                Id = odwolaniePosilku.Id,
                                DzieckoId = odwolaniePosilku.DzieckoId,
                                DataPosilku = odwolaniePosilku.DataPosilku,
                                DataZgloszenia = odwolaniePosilku.DataZgloszenia
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OdwolaniePosilkuDto> CreateAsync(OdwolaniePosilkuDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new OdwolaniePosilku
        {
            DzieckoId = dto.DzieckoId,
            DataPosilku = dto.DataPosilku,
            DataZgloszenia = dto.DataZgloszenia,
        };
        _dbContext.OdwolaniaPosilkow.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, OdwolaniePosilkuDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.OdwolaniaPosilkow.FirstOrDefaultAsync(odwolaniePosilku => odwolaniePosilku.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.DzieckoId = dto.DzieckoId;
        entity.DataPosilku = dto.DataPosilku;
        entity.DataZgloszenia = dto.DataZgloszenia;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.OdwolaniaPosilkow.FirstOrDefaultAsync(odwolaniePosilku => odwolaniePosilku.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.OdwolaniaPosilkow.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static OdwolaniePosilkuDto MapToDto(OdwolaniePosilku entity)
    {
        return new OdwolaniePosilkuDto
        {
            Id = entity.Id,
                            DzieckoId = entity.DzieckoId,
                            DataPosilku = entity.DataPosilku,
                            DataZgloszenia = entity.DataZgloszenia
        };
    }
}
