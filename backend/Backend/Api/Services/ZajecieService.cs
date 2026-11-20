using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class ZajecieService : IZajecieService
{
    private readonly ApplicationDbContext _dbContext;

    public ZajecieService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<ZajecieDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Zajecia.AsNoTracking()
            .OrderBy(zajecie => zajecie.Id)
            .Select(zajecie => new ZajecieDto
            {
                Id = zajecie.Id,
                                Nazwa = zajecie.Nazwa,
                                TypZajec = zajecie.TypZajec
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<ZajecieDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Zajecia.AsNoTracking()
            .Where(zajecie => zajecie.Id == id)
            .Select(zajecie => new ZajecieDto
            {
                Id = zajecie.Id,
                                Nazwa = zajecie.Nazwa,
                                TypZajec = zajecie.TypZajec
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ZajecieDto> CreateAsync(ZajecieDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Zajecie
        {
            Nazwa = dto.Nazwa,
            TypZajec = dto.TypZajec,
        };
        _dbContext.Zajecia.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, ZajecieDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Zajecia.FirstOrDefaultAsync(zajecie => zajecie.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.Nazwa = dto.Nazwa;
        entity.TypZajec = dto.TypZajec;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Zajecia.FirstOrDefaultAsync(zajecie => zajecie.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.Zajecia.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static ZajecieDto MapToDto(Zajecie entity)
    {
        return new ZajecieDto
        {
            Id = entity.Id,
                            Nazwa = entity.Nazwa,
                            TypZajec = entity.TypZajec
        };
    }
}
