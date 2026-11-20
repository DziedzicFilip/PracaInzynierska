using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class OsobaUpowaznionaOdbiorService : IOsobaUpowaznionaOdbiorService
{
    private readonly ApplicationDbContext _dbContext;

    public OsobaUpowaznionaOdbiorService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<OsobaUpowaznionaOdbiorDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.OsobyUpowaznioneOdbior.AsNoTracking()
            .OrderBy(osobaUpowaznionaOdbior => osobaUpowaznionaOdbior.Id)
            .Select(osobaUpowaznionaOdbior => new OsobaUpowaznionaOdbiorDto
            {
                Id = osobaUpowaznionaOdbior.Id,
                                DzieckoId = osobaUpowaznionaOdbior.DzieckoId,
                                Imie = osobaUpowaznionaOdbior.Imie,
                                Nazwisko = osobaUpowaznionaOdbior.Nazwisko,
                                JednorazowyKodPin = osobaUpowaznionaOdbior.JednorazowyKodPin
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<OsobaUpowaznionaOdbiorDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.OsobyUpowaznioneOdbior.AsNoTracking()
            .Where(osobaUpowaznionaOdbior => osobaUpowaznionaOdbior.Id == id)
            .Select(osobaUpowaznionaOdbior => new OsobaUpowaznionaOdbiorDto
            {
                Id = osobaUpowaznionaOdbior.Id,
                                DzieckoId = osobaUpowaznionaOdbior.DzieckoId,
                                Imie = osobaUpowaznionaOdbior.Imie,
                                Nazwisko = osobaUpowaznionaOdbior.Nazwisko,
                                JednorazowyKodPin = osobaUpowaznionaOdbior.JednorazowyKodPin
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OsobaUpowaznionaOdbiorDto> CreateAsync(OsobaUpowaznionaOdbiorDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new OsobaUpowaznionaOdbior
        {
            DzieckoId = dto.DzieckoId,
            Imie = dto.Imie,
            Nazwisko = dto.Nazwisko,
            JednorazowyKodPin = dto.JednorazowyKodPin,
        };
        _dbContext.OsobyUpowaznioneOdbior.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, OsobaUpowaznionaOdbiorDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.OsobyUpowaznioneOdbior.FirstOrDefaultAsync(osobaUpowaznionaOdbior => osobaUpowaznionaOdbior.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.DzieckoId = dto.DzieckoId;
        entity.Imie = dto.Imie;
        entity.Nazwisko = dto.Nazwisko;
        entity.JednorazowyKodPin = dto.JednorazowyKodPin;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.OsobyUpowaznioneOdbior.FirstOrDefaultAsync(osobaUpowaznionaOdbior => osobaUpowaznionaOdbior.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.OsobyUpowaznioneOdbior.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static OsobaUpowaznionaOdbiorDto MapToDto(OsobaUpowaznionaOdbior entity)
    {
        return new OsobaUpowaznionaOdbiorDto
        {
            Id = entity.Id,
                            DzieckoId = entity.DzieckoId,
                            Imie = entity.Imie,
                            Nazwisko = entity.Nazwisko,
                            JednorazowyKodPin = entity.JednorazowyKodPin
        };
    }
}
