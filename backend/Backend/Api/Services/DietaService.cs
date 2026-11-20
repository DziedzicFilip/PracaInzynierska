using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class DietaService : IDietaService
{
    private readonly ApplicationDbContext _dbContext;

    public DietaService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<DietaDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Diety.AsNoTracking()
            .OrderBy(dieta => dieta.Id)
            .Select(dieta => new DietaDto
            {
                Id = dieta.Id,
                                Nazwa = dieta.Nazwa
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<DietaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Diety.AsNoTracking()
            .Where(dieta => dieta.Id == id)
            .Select(dieta => new DietaDto
            {
                Id = dieta.Id,
                                Nazwa = dieta.Nazwa
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<DietaDto> CreateAsync(DietaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Dieta
        {
            Nazwa = dto.Nazwa,
        };
        _dbContext.Diety.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, DietaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Diety.FirstOrDefaultAsync(dieta => dieta.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.Nazwa = dto.Nazwa;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Diety.FirstOrDefaultAsync(dieta => dieta.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.Diety.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static DietaDto MapToDto(Dieta entity)
    {
        return new DietaDto
        {
            Id = entity.Id,
                            Nazwa = entity.Nazwa
        };
    }
}
