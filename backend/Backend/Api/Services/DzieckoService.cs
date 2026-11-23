using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class DzieckoService : IDzieckoService
{
    private readonly ApplicationDbContext _dbContext;

    public DzieckoService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<DzieckoDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Dzieci.AsNoTracking()
            .OrderBy(dziecko => dziecko.Id)
            .Select(dziecko => new DzieckoDto
            {
                Id = dziecko.Id,
                                Imie = dziecko.Imie,
                                Nazwisko = dziecko.Nazwisko,
                                DataUrodzenia = dziecko.DataUrodzenia,
                                GrupaId = dziecko.GrupaId
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<DzieckoDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Dzieci.AsNoTracking()
            .Where(dziecko => dziecko.Id == id)
            .Select(dziecko => new DzieckoDto
            {
                Id = dziecko.Id,
                                Imie = dziecko.Imie,
                                Nazwisko = dziecko.Nazwisko,
                                DataUrodzenia = dziecko.DataUrodzenia,
                                GrupaId = dziecko.GrupaId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<DzieckoDto> CreateAsync(DzieckoDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Dziecko
        {
            Imie = dto.Imie,
            Nazwisko = dto.Nazwisko,
            DataUrodzenia = dto.DataUrodzenia,
            GrupaId = dto.GrupaId,
        };
        _dbContext.Dzieci.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, DzieckoDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Dzieci.FirstOrDefaultAsync(dziecko => dziecko.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.Imie = dto.Imie;
        entity.Nazwisko = dto.Nazwisko;
        entity.DataUrodzenia = dto.DataUrodzenia;
        entity.GrupaId = dto.GrupaId;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Dzieci.FirstOrDefaultAsync(dziecko => dziecko.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.Dzieci.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static DzieckoDto MapToDto(Dziecko entity)
    {
        return new DzieckoDto
        {
            Id = entity.Id,
                            Imie = entity.Imie,
                            Nazwisko = entity.Nazwisko,
                            DataUrodzenia = entity.DataUrodzenia,
                            GrupaId = entity.GrupaId
        };
    }
}
