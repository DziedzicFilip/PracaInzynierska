using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class ObecnoscService : IObecnoscService
{
    private readonly ApplicationDbContext _dbContext;

    public ObecnoscService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<ObecnoscDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Obecnosci.AsNoTracking()
            .OrderBy(obecnosc => obecnosc.Id)
            .Select(obecnosc => new ObecnoscDto
            {
                Id = obecnosc.Id,
                                DzieckoId = obecnosc.DzieckoId,
                                Data = obecnosc.Data,
                                Status = obecnosc.Status,
                                GodzinaPrzyjscia = obecnosc.GodzinaPrzyjscia,
                                GodzinaOdbioru = obecnosc.GodzinaOdbioru,
                                RodzicOdbierajacyId = obecnosc.RodzicOdbierajacyId,
                                OsobaUpowaznionaId = obecnosc.OsobaUpowaznionaId,
                                MetodaRejestracji = obecnosc.MetodaRejestracji
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<ObecnoscDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Obecnosci.AsNoTracking()
            .Where(obecnosc => obecnosc.Id == id)
            .Select(obecnosc => new ObecnoscDto
            {
                Id = obecnosc.Id,
                                DzieckoId = obecnosc.DzieckoId,
                                Data = obecnosc.Data,
                                Status = obecnosc.Status,
                                GodzinaPrzyjscia = obecnosc.GodzinaPrzyjscia,
                                GodzinaOdbioru = obecnosc.GodzinaOdbioru,
                                RodzicOdbierajacyId = obecnosc.RodzicOdbierajacyId,
                                OsobaUpowaznionaId = obecnosc.OsobaUpowaznionaId,
                                MetodaRejestracji = obecnosc.MetodaRejestracji
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ObecnoscDto> CreateAsync(ObecnoscDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Obecnosc
        {
            DzieckoId = dto.DzieckoId,
            Data = dto.Data,
            Status = dto.Status,
            GodzinaPrzyjscia = dto.GodzinaPrzyjscia,
            GodzinaOdbioru = dto.GodzinaOdbioru,
            RodzicOdbierajacyId = dto.RodzicOdbierajacyId,
            OsobaUpowaznionaId = dto.OsobaUpowaznionaId,
            MetodaRejestracji = dto.MetodaRejestracji,
        };
        _dbContext.Obecnosci.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, ObecnoscDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Obecnosci.FirstOrDefaultAsync(obecnosc => obecnosc.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.DzieckoId = dto.DzieckoId;
        entity.Data = dto.Data;
        entity.Status = dto.Status;
        entity.GodzinaPrzyjscia = dto.GodzinaPrzyjscia;
        entity.GodzinaOdbioru = dto.GodzinaOdbioru;
        entity.RodzicOdbierajacyId = dto.RodzicOdbierajacyId;
        entity.OsobaUpowaznionaId = dto.OsobaUpowaznionaId;
        entity.MetodaRejestracji = dto.MetodaRejestracji;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Obecnosci.FirstOrDefaultAsync(obecnosc => obecnosc.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.Obecnosci.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static ObecnoscDto MapToDto(Obecnosc entity)
    {
        return new ObecnoscDto
        {
            Id = entity.Id,
                            DzieckoId = entity.DzieckoId,
                            Data = entity.Data,
                            Status = entity.Status,
                            GodzinaPrzyjscia = entity.GodzinaPrzyjscia,
                            GodzinaOdbioru = entity.GodzinaOdbioru,
                            RodzicOdbierajacyId = entity.RodzicOdbierajacyId,
                            OsobaUpowaznionaId = entity.OsobaUpowaznionaId,
                            MetodaRejestracji = entity.MetodaRejestracji
        };
    }
}
