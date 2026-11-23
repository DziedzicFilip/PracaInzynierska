using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class GrafikPracyService : IGrafikPracyService
{
    private readonly ApplicationDbContext _dbContext;

    public GrafikPracyService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<GrafikPracyDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.GrafikPracy.AsNoTracking()
            .OrderBy(grafikPracy => grafikPracy.Id)
            .Select(grafikPracy => new GrafikPracyDto
            {
                Id = grafikPracy.Id,
                                PracownikId = grafikPracy.PracownikId,
                                Data = grafikPracy.Data,
                                GodzinaRozpoczecia = grafikPracy.GodzinaRozpoczecia,
                                GodzinaZakonczenia = grafikPracy.GodzinaZakonczenia
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<GrafikPracyDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.GrafikPracy.AsNoTracking()
            .Where(grafikPracy => grafikPracy.Id == id)
            .Select(grafikPracy => new GrafikPracyDto
            {
                Id = grafikPracy.Id,
                                PracownikId = grafikPracy.PracownikId,
                                Data = grafikPracy.Data,
                                GodzinaRozpoczecia = grafikPracy.GodzinaRozpoczecia,
                                GodzinaZakonczenia = grafikPracy.GodzinaZakonczenia
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<GrafikPracyDto> CreateAsync(GrafikPracyDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new GrafikPracy
        {
            PracownikId = dto.PracownikId,
            Data = dto.Data,
            GodzinaRozpoczecia = dto.GodzinaRozpoczecia,
            GodzinaZakonczenia = dto.GodzinaZakonczenia,
        };
        _dbContext.GrafikPracy.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, GrafikPracyDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.GrafikPracy.FirstOrDefaultAsync(grafikPracy => grafikPracy.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.PracownikId = dto.PracownikId;
        entity.Data = dto.Data;
        entity.GodzinaRozpoczecia = dto.GodzinaRozpoczecia;
        entity.GodzinaZakonczenia = dto.GodzinaZakonczenia;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.GrafikPracy.FirstOrDefaultAsync(grafikPracy => grafikPracy.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.GrafikPracy.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static GrafikPracyDto MapToDto(GrafikPracy entity)
    {
        return new GrafikPracyDto
        {
            Id = entity.Id,
                            PracownikId = entity.PracownikId,
                            Data = entity.Data,
                            GodzinaRozpoczecia = entity.GodzinaRozpoczecia,
                            GodzinaZakonczenia = entity.GodzinaZakonczenia
        };
    }
}
