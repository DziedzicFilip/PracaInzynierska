using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class OgloszenieService : IOgloszenieService
{
    private readonly ApplicationDbContext _dbContext;

    public OgloszenieService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<OgloszenieDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Ogloszenia.AsNoTracking()
            .OrderBy(ogloszenie => ogloszenie.Id)
            .Select(ogloszenie => new OgloszenieDto
            {
                Id = ogloszenie.Id,
                                AutorId = ogloszenie.AutorId,
                                GrupaId = ogloszenie.GrupaId,
                                Tytul = ogloszenie.Tytul,
                                Tresc = ogloszenie.Tresc,
                                DataPublikacji = ogloszenie.DataPublikacji
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<OgloszenieDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Ogloszenia.AsNoTracking()
            .Where(ogloszenie => ogloszenie.Id == id)
            .Select(ogloszenie => new OgloszenieDto
            {
                Id = ogloszenie.Id,
                                AutorId = ogloszenie.AutorId,
                                GrupaId = ogloszenie.GrupaId,
                                Tytul = ogloszenie.Tytul,
                                Tresc = ogloszenie.Tresc,
                                DataPublikacji = ogloszenie.DataPublikacji
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OgloszenieDto> CreateAsync(OgloszenieDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Ogloszenie
        {
            AutorId = dto.AutorId,
            GrupaId = dto.GrupaId,
            Tytul = dto.Tytul,
            Tresc = dto.Tresc,
            DataPublikacji = dto.DataPublikacji,
        };
        _dbContext.Ogloszenia.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, OgloszenieDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Ogloszenia.FirstOrDefaultAsync(ogloszenie => ogloszenie.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.AutorId = dto.AutorId;
        entity.GrupaId = dto.GrupaId;
        entity.Tytul = dto.Tytul;
        entity.Tresc = dto.Tresc;
        entity.DataPublikacji = dto.DataPublikacji;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Ogloszenia.FirstOrDefaultAsync(ogloszenie => ogloszenie.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.Ogloszenia.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static OgloszenieDto MapToDto(Ogloszenie entity)
    {
        return new OgloszenieDto
        {
            Id = entity.Id,
                            AutorId = entity.AutorId,
                            GrupaId = entity.GrupaId,
                            Tytul = entity.Tytul,
                            Tresc = entity.Tresc,
                            DataPublikacji = entity.DataPublikacji
        };
    }
}
