using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class RodzicService : IRodzicService
{
    private readonly ApplicationDbContext _dbContext;

    public RodzicService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<RodzicDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Rodzice.AsNoTracking()
            .OrderBy(rodzic => rodzic.Id)
            .Select(rodzic => new RodzicDto
            {
                Id = rodzic.Id,
                                UzytkownikId = rodzic.UzytkownikId,
                                Imie = rodzic.Imie,
                                Nazwisko = rodzic.Nazwisko,
                                KodPinOdbior = rodzic.KodPinOdbior
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<RodzicDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Rodzice.AsNoTracking()
            .Where(rodzic => rodzic.Id == id)
            .Select(rodzic => new RodzicDto
            {
                Id = rodzic.Id,
                                UzytkownikId = rodzic.UzytkownikId,
                                Imie = rodzic.Imie,
                                Nazwisko = rodzic.Nazwisko,
                                KodPinOdbior = rodzic.KodPinOdbior
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<RodzicDto> CreateAsync(RodzicDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Rodzic
        {
            UzytkownikId = dto.UzytkownikId,
            Imie = dto.Imie,
            Nazwisko = dto.Nazwisko,
            KodPinOdbior = dto.KodPinOdbior,
        };
        _dbContext.Rodzice.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, RodzicDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Rodzice.FirstOrDefaultAsync(rodzic => rodzic.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.UzytkownikId = dto.UzytkownikId;
        entity.Imie = dto.Imie;
        entity.Nazwisko = dto.Nazwisko;
        entity.KodPinOdbior = dto.KodPinOdbior;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Rodzice.FirstOrDefaultAsync(rodzic => rodzic.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.Rodzice.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static RodzicDto MapToDto(Rodzic entity)
    {
        return new RodzicDto
        {
            Id = entity.Id,
                            UzytkownikId = entity.UzytkownikId,
                            Imie = entity.Imie,
                            Nazwisko = entity.Nazwisko,
                            KodPinOdbior = entity.KodPinOdbior
        };
    }
}
