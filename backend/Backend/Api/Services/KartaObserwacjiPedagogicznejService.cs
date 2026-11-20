using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class KartaObserwacjiPedagogicznejService : IKartaObserwacjiPedagogicznejService
{
    private readonly ApplicationDbContext _dbContext;

    public KartaObserwacjiPedagogicznejService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<KartaObserwacjiPedagogicznejDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.KartyObserwacjiPedagogicznej.AsNoTracking()
            .OrderBy(kartaObserwacjiPedagogicznej => kartaObserwacjiPedagogicznej.Id)
            .Select(kartaObserwacjiPedagogicznej => new KartaObserwacjiPedagogicznejDto
            {
                Id = kartaObserwacjiPedagogicznej.Id,
                                DzieckoId = kartaObserwacjiPedagogicznej.DzieckoId,
                                PracownikId = kartaObserwacjiPedagogicznej.PracownikId,
                                Specjalizacja = kartaObserwacjiPedagogicznej.Specjalizacja,
                                Notatka = kartaObserwacjiPedagogicznej.Notatka,
                                DataWpisu = kartaObserwacjiPedagogicznej.DataWpisu
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<KartaObserwacjiPedagogicznejDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.KartyObserwacjiPedagogicznej.AsNoTracking()
            .Where(kartaObserwacjiPedagogicznej => kartaObserwacjiPedagogicznej.Id == id)
            .Select(kartaObserwacjiPedagogicznej => new KartaObserwacjiPedagogicznejDto
            {
                Id = kartaObserwacjiPedagogicznej.Id,
                                DzieckoId = kartaObserwacjiPedagogicznej.DzieckoId,
                                PracownikId = kartaObserwacjiPedagogicznej.PracownikId,
                                Specjalizacja = kartaObserwacjiPedagogicznej.Specjalizacja,
                                Notatka = kartaObserwacjiPedagogicznej.Notatka,
                                DataWpisu = kartaObserwacjiPedagogicznej.DataWpisu
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<KartaObserwacjiPedagogicznejDto> CreateAsync(KartaObserwacjiPedagogicznejDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new KartaObserwacjiPedagogicznej
        {
            DzieckoId = dto.DzieckoId,
            PracownikId = dto.PracownikId,
            Specjalizacja = dto.Specjalizacja,
            Notatka = dto.Notatka,
            DataWpisu = dto.DataWpisu,
        };
        _dbContext.KartyObserwacjiPedagogicznej.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, KartaObserwacjiPedagogicznejDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.KartyObserwacjiPedagogicznej.FirstOrDefaultAsync(kartaObserwacjiPedagogicznej => kartaObserwacjiPedagogicznej.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.DzieckoId = dto.DzieckoId;
        entity.PracownikId = dto.PracownikId;
        entity.Specjalizacja = dto.Specjalizacja;
        entity.Notatka = dto.Notatka;
        entity.DataWpisu = dto.DataWpisu;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.KartyObserwacjiPedagogicznej.FirstOrDefaultAsync(kartaObserwacjiPedagogicznej => kartaObserwacjiPedagogicznej.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.KartyObserwacjiPedagogicznej.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static KartaObserwacjiPedagogicznejDto MapToDto(KartaObserwacjiPedagogicznej entity)
    {
        return new KartaObserwacjiPedagogicznejDto
        {
            Id = entity.Id,
                            DzieckoId = entity.DzieckoId,
                            PracownikId = entity.PracownikId,
                            Specjalizacja = entity.Specjalizacja,
                            Notatka = entity.Notatka,
                            DataWpisu = entity.DataWpisu
        };
    }
}
