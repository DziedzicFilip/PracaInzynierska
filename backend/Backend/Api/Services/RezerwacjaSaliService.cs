using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class RezerwacjaSaliService : IRezerwacjaSaliService
{
    private readonly ApplicationDbContext _dbContext;

    public RezerwacjaSaliService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<RezerwacjaSaliDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.RezerwacjeSal.AsNoTracking()
            .OrderBy(rezerwacjaSali => rezerwacjaSali.Id)
            .Select(rezerwacjaSali => new RezerwacjaSaliDto
            {
                Id = rezerwacjaSali.Id,
                                SalaId = rezerwacjaSali.SalaId,
                                PracownikId = rezerwacjaSali.PracownikId,
                                Cel = rezerwacjaSali.Cel,
                                DataRezerwacji = rezerwacjaSali.DataRezerwacji,
                                GodzinaOd = rezerwacjaSali.GodzinaOd,
                                GodzinaDo = rezerwacjaSali.GodzinaDo
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<RezerwacjaSaliDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.RezerwacjeSal.AsNoTracking()
            .Where(rezerwacjaSali => rezerwacjaSali.Id == id)
            .Select(rezerwacjaSali => new RezerwacjaSaliDto
            {
                Id = rezerwacjaSali.Id,
                                SalaId = rezerwacjaSali.SalaId,
                                PracownikId = rezerwacjaSali.PracownikId,
                                Cel = rezerwacjaSali.Cel,
                                DataRezerwacji = rezerwacjaSali.DataRezerwacji,
                                GodzinaOd = rezerwacjaSali.GodzinaOd,
                                GodzinaDo = rezerwacjaSali.GodzinaDo
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<RezerwacjaSaliDto> CreateAsync(RezerwacjaSaliDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new RezerwacjaSali
        {
            SalaId = dto.SalaId,
            PracownikId = dto.PracownikId,
            Cel = dto.Cel,
            DataRezerwacji = dto.DataRezerwacji,
            GodzinaOd = dto.GodzinaOd,
            GodzinaDo = dto.GodzinaDo,
        };
        _dbContext.RezerwacjeSal.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, RezerwacjaSaliDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.RezerwacjeSal.FirstOrDefaultAsync(rezerwacjaSali => rezerwacjaSali.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.SalaId = dto.SalaId;
        entity.PracownikId = dto.PracownikId;
        entity.Cel = dto.Cel;
        entity.DataRezerwacji = dto.DataRezerwacji;
        entity.GodzinaOd = dto.GodzinaOd;
        entity.GodzinaDo = dto.GodzinaDo;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.RezerwacjeSal.FirstOrDefaultAsync(rezerwacjaSali => rezerwacjaSali.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.RezerwacjeSal.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static RezerwacjaSaliDto MapToDto(RezerwacjaSali entity)
    {
        return new RezerwacjaSaliDto
        {
            Id = entity.Id,
                            SalaId = entity.SalaId,
                            PracownikId = entity.PracownikId,
                            Cel = entity.Cel,
                            DataRezerwacji = entity.DataRezerwacji,
                            GodzinaOd = entity.GodzinaOd,
                            GodzinaDo = entity.GodzinaDo
        };
    }
}
