using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class RezerwacjaPomocyService : IRezerwacjaPomocyService
{
    private readonly ApplicationDbContext _dbContext;

    public RezerwacjaPomocyService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<RezerwacjaPomocyDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.RezerwacjePomocyDydaktycznych.AsNoTracking()
            .OrderBy(rezerwacjaPomocy => rezerwacjaPomocy.Id)
            .Select(rezerwacjaPomocy => new RezerwacjaPomocyDto
            {
                Id = rezerwacjaPomocy.Id,
                                PomocId = rezerwacjaPomocy.PomocId,
                                PracownikId = rezerwacjaPomocy.PracownikId,
                                DataOd = rezerwacjaPomocy.DataOd,
                                DataDo = rezerwacjaPomocy.DataDo
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<RezerwacjaPomocyDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.RezerwacjePomocyDydaktycznych.AsNoTracking()
            .Where(rezerwacjaPomocy => rezerwacjaPomocy.Id == id)
            .Select(rezerwacjaPomocy => new RezerwacjaPomocyDto
            {
                Id = rezerwacjaPomocy.Id,
                                PomocId = rezerwacjaPomocy.PomocId,
                                PracownikId = rezerwacjaPomocy.PracownikId,
                                DataOd = rezerwacjaPomocy.DataOd,
                                DataDo = rezerwacjaPomocy.DataDo
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<RezerwacjaPomocyDto> CreateAsync(RezerwacjaPomocyDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new RezerwacjaPomocy
        {
            PomocId = dto.PomocId,
            PracownikId = dto.PracownikId,
            DataOd = dto.DataOd,
            DataDo = dto.DataDo,
        };
        _dbContext.RezerwacjePomocyDydaktycznych.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, RezerwacjaPomocyDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.RezerwacjePomocyDydaktycznych.FirstOrDefaultAsync(rezerwacjaPomocy => rezerwacjaPomocy.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.PomocId = dto.PomocId;
        entity.PracownikId = dto.PracownikId;
        entity.DataOd = dto.DataOd;
        entity.DataDo = dto.DataDo;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.RezerwacjePomocyDydaktycznych.FirstOrDefaultAsync(rezerwacjaPomocy => rezerwacjaPomocy.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.RezerwacjePomocyDydaktycznych.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static RezerwacjaPomocyDto MapToDto(RezerwacjaPomocy entity)
    {
        return new RezerwacjaPomocyDto
        {
            Id = entity.Id,
                            PomocId = entity.PomocId,
                            PracownikId = entity.PracownikId,
                            DataOd = entity.DataOd,
                            DataDo = entity.DataDo
        };
    }
}
