using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class RodzicDzieckoService : IRodzicDzieckoService
{
    private readonly ApplicationDbContext _dbContext;

    public RodzicDzieckoService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<RodzicDzieckoDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.RodzicDziecko.AsNoTracking()
            .OrderBy(rodzicDziecko => rodzicDziecko.RodzicId)
            .Select(rodzicDziecko => new RodzicDzieckoDto
            {
                RodzicId = rodzicDziecko.RodzicId,
                                DzieckoId = rodzicDziecko.DzieckoId
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<RodzicDzieckoDto?> GetByIdAsync(int rodzicId, int dzieckoId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.RodzicDziecko.AsNoTracking()
            .Where(rodzicDziecko => rodzicDziecko.RodzicId == rodzicId && rodzicDziecko.DzieckoId == dzieckoId)
            .Select(rodzicDziecko => new RodzicDzieckoDto
            {
                RodzicId = rodzicDziecko.RodzicId,
                                DzieckoId = rodzicDziecko.DzieckoId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<RodzicDzieckoDto> CreateAsync(RodzicDzieckoDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new RodzicDziecko
        {
            RodzicId = dto.RodzicId,
            DzieckoId = dto.DzieckoId,
        };
        _dbContext.RodzicDziecko.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int rodzicId, int dzieckoId, RodzicDzieckoDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.RodzicDziecko.FirstOrDefaultAsync(rodzicDziecko => rodzicDziecko.RodzicId == rodzicId && rodzicDziecko.DzieckoId == dzieckoId, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int rodzicId, int dzieckoId, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.RodzicDziecko.FirstOrDefaultAsync(rodzicDziecko => rodzicDziecko.RodzicId == rodzicId && rodzicDziecko.DzieckoId == dzieckoId, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.RodzicDziecko.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static RodzicDzieckoDto MapToDto(RodzicDziecko entity)
    {
        return new RodzicDzieckoDto
        {
            RodzicId = entity.RodzicId,
                            DzieckoId = entity.DzieckoId
        };
    }
}
