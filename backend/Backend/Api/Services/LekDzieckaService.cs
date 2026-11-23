using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class LekDzieckaService : ILekDzieckaService
{
    private readonly ApplicationDbContext _dbContext;

    public LekDzieckaService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<LekDzieckaDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.LekiDzieci.AsNoTracking()
            .OrderBy(lekDziecka => lekDziecka.Id)
            .Select(lekDziecka => new LekDzieckaDto
            {
                Id = lekDziecka.Id,
                                DzieckoId = lekDziecka.DzieckoId,
                                NazwaLeku = lekDziecka.NazwaLeku,
                                Dawka = lekDziecka.Dawka,
                                GodzinaPodania = lekDziecka.GodzinaPodania,
                                DataOd = lekDziecka.DataOd,
                                DataDo = lekDziecka.DataDo,
                                Uwagi = lekDziecka.Uwagi,
                                ZgodaRodzica = lekDziecka.ZgodaRodzica
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<LekDzieckaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.LekiDzieci.AsNoTracking()
            .Where(lekDziecka => lekDziecka.Id == id)
            .Select(lekDziecka => new LekDzieckaDto
            {
                Id = lekDziecka.Id,
                                DzieckoId = lekDziecka.DzieckoId,
                                NazwaLeku = lekDziecka.NazwaLeku,
                                Dawka = lekDziecka.Dawka,
                                GodzinaPodania = lekDziecka.GodzinaPodania,
                                DataOd = lekDziecka.DataOd,
                                DataDo = lekDziecka.DataDo,
                                Uwagi = lekDziecka.Uwagi,
                                ZgodaRodzica = lekDziecka.ZgodaRodzica
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<LekDzieckaDto> CreateAsync(LekDzieckaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new LekDziecka
        {
            DzieckoId = dto.DzieckoId,
            NazwaLeku = dto.NazwaLeku,
            Dawka = dto.Dawka,
            GodzinaPodania = dto.GodzinaPodania,
            DataOd = dto.DataOd,
            DataDo = dto.DataDo,
            Uwagi = dto.Uwagi,
            ZgodaRodzica = dto.ZgodaRodzica,
        };
        _dbContext.LekiDzieci.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, LekDzieckaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.LekiDzieci.FirstOrDefaultAsync(lekDziecka => lekDziecka.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.DzieckoId = dto.DzieckoId;
        entity.NazwaLeku = dto.NazwaLeku;
        entity.Dawka = dto.Dawka;
        entity.GodzinaPodania = dto.GodzinaPodania;
        entity.DataOd = dto.DataOd;
        entity.DataDo = dto.DataDo;
        entity.Uwagi = dto.Uwagi;
        entity.ZgodaRodzica = dto.ZgodaRodzica;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.LekiDzieci.FirstOrDefaultAsync(lekDziecka => lekDziecka.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.LekiDzieci.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static LekDzieckaDto MapToDto(LekDziecka entity)
    {
        return new LekDzieckaDto
        {
            Id = entity.Id,
                            DzieckoId = entity.DzieckoId,
                            NazwaLeku = entity.NazwaLeku,
                            Dawka = entity.Dawka,
                            GodzinaPodania = entity.GodzinaPodania,
                            DataOd = entity.DataOd,
                            DataDo = entity.DataDo,
                            Uwagi = entity.Uwagi,
                            ZgodaRodzica = entity.ZgodaRodzica
        };
    }
}
