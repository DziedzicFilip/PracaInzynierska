using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PracownikService : IPracownikService
{
    private readonly ApplicationDbContext _dbContext;

    public PracownikService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<PracownikDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Pracownicy.AsNoTracking()
            .OrderBy(pracownik => pracownik.Id)
            .Select(pracownik => new PracownikDto
            {
                Id = pracownik.Id,
                                UzytkownikId = pracownik.UzytkownikId,
                                Imie = pracownik.Imie,
                                Nazwisko = pracownik.Nazwisko
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<PracownikDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Pracownicy.AsNoTracking()
            .Where(pracownik => pracownik.Id == id)
            .Select(pracownik => new PracownikDto
            {
                Id = pracownik.Id,
                                UzytkownikId = pracownik.UzytkownikId,
                                Imie = pracownik.Imie,
                                Nazwisko = pracownik.Nazwisko
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PracownikDto> CreateAsync(PracownikDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Pracownik
        {
            UzytkownikId = dto.UzytkownikId,
            Imie = dto.Imie,
            Nazwisko = dto.Nazwisko,
        };
        _dbContext.Pracownicy.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, PracownikDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Pracownicy.FirstOrDefaultAsync(pracownik => pracownik.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.UzytkownikId = dto.UzytkownikId;
        entity.Imie = dto.Imie;
        entity.Nazwisko = dto.Nazwisko;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Pracownicy.FirstOrDefaultAsync(pracownik => pracownik.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.Pracownicy.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static PracownikDto MapToDto(Pracownik entity)
    {
        return new PracownikDto
        {
            Id = entity.Id,
                            UzytkownikId = entity.UzytkownikId,
                            Imie = entity.Imie,
                            Nazwisko = entity.Nazwisko
        };
    }
}
