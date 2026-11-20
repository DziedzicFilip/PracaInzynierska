using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class BadanieSzkoleniePracownikaService : IBadanieSzkoleniePracownikaService
{
    private readonly ApplicationDbContext _dbContext;

    public BadanieSzkoleniePracownikaService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<BadanieSzkoleniePracownikaDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.BadaniaSzkoleniaPracownikow.AsNoTracking()
            .OrderBy(badanieSzkoleniePracownika => badanieSzkoleniePracownika.Id)
            .Select(badanieSzkoleniePracownika => new BadanieSzkoleniePracownikaDto
            {
                Id = badanieSzkoleniePracownika.Id,
                                PracownikId = badanieSzkoleniePracownika.PracownikId,
                                Typ = badanieSzkoleniePracownika.Typ,
                                DataWaznosci = badanieSzkoleniePracownika.DataWaznosci
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<BadanieSzkoleniePracownikaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.BadaniaSzkoleniaPracownikow.AsNoTracking()
            .Where(badanieSzkoleniePracownika => badanieSzkoleniePracownika.Id == id)
            .Select(badanieSzkoleniePracownika => new BadanieSzkoleniePracownikaDto
            {
                Id = badanieSzkoleniePracownika.Id,
                                PracownikId = badanieSzkoleniePracownika.PracownikId,
                                Typ = badanieSzkoleniePracownika.Typ,
                                DataWaznosci = badanieSzkoleniePracownika.DataWaznosci
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<BadanieSzkoleniePracownikaDto> CreateAsync(BadanieSzkoleniePracownikaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new BadanieSzkoleniePracownika
        {
            PracownikId = dto.PracownikId,
            Typ = dto.Typ,
            DataWaznosci = dto.DataWaznosci,
        };
        _dbContext.BadaniaSzkoleniaPracownikow.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, BadanieSzkoleniePracownikaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.BadaniaSzkoleniaPracownikow.FirstOrDefaultAsync(badanieSzkoleniePracownika => badanieSzkoleniePracownika.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.PracownikId = dto.PracownikId;
        entity.Typ = dto.Typ;
        entity.DataWaznosci = dto.DataWaznosci;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.BadaniaSzkoleniaPracownikow.FirstOrDefaultAsync(badanieSzkoleniePracownika => badanieSzkoleniePracownika.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.BadaniaSzkoleniaPracownikow.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static BadanieSzkoleniePracownikaDto MapToDto(BadanieSzkoleniePracownika entity)
    {
        return new BadanieSzkoleniePracownikaDto
        {
            Id = entity.Id,
                            PracownikId = entity.PracownikId,
                            Typ = entity.Typ,
                            DataWaznosci = entity.DataWaznosci
        };
    }
}
