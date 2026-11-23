using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PomocDydaktycznaService : IPomocDydaktycznaService
{
    private readonly ApplicationDbContext _dbContext;

    public PomocDydaktycznaService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<PomocDydaktycznaDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.PomoceDydaktyczne.AsNoTracking()
            .OrderBy(pomocDydaktyczna => pomocDydaktyczna.Id)
            .Select(pomocDydaktyczna => new PomocDydaktycznaDto
            {
                Id = pomocDydaktyczna.Id,
                                Nazwa = pomocDydaktyczna.Nazwa,
                                Typ = pomocDydaktyczna.Typ,
                                StanIlosciowy = pomocDydaktyczna.StanIlosciowy,
                                SalaPrzypisanaId = pomocDydaktyczna.SalaPrzypisanaId
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<PomocDydaktycznaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.PomoceDydaktyczne.AsNoTracking()
            .Where(pomocDydaktyczna => pomocDydaktyczna.Id == id)
            .Select(pomocDydaktyczna => new PomocDydaktycznaDto
            {
                Id = pomocDydaktyczna.Id,
                                Nazwa = pomocDydaktyczna.Nazwa,
                                Typ = pomocDydaktyczna.Typ,
                                StanIlosciowy = pomocDydaktyczna.StanIlosciowy,
                                SalaPrzypisanaId = pomocDydaktyczna.SalaPrzypisanaId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PomocDydaktycznaDto> CreateAsync(PomocDydaktycznaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new PomocDydaktyczna
        {
            Nazwa = dto.Nazwa,
            Typ = dto.Typ,
            StanIlosciowy = dto.StanIlosciowy,
            SalaPrzypisanaId = dto.SalaPrzypisanaId,
        };
        _dbContext.PomoceDydaktyczne.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, PomocDydaktycznaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.PomoceDydaktyczne.FirstOrDefaultAsync(pomocDydaktyczna => pomocDydaktyczna.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.Nazwa = dto.Nazwa;
        entity.Typ = dto.Typ;
        entity.StanIlosciowy = dto.StanIlosciowy;
        entity.SalaPrzypisanaId = dto.SalaPrzypisanaId;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.PomoceDydaktyczne.FirstOrDefaultAsync(pomocDydaktyczna => pomocDydaktyczna.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.PomoceDydaktyczne.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static PomocDydaktycznaDto MapToDto(PomocDydaktyczna entity)
    {
        return new PomocDydaktycznaDto
        {
            Id = entity.Id,
                            Nazwa = entity.Nazwa,
                            Typ = entity.Typ,
                            StanIlosciowy = entity.StanIlosciowy,
                            SalaPrzypisanaId = entity.SalaPrzypisanaId
        };
    }
}
