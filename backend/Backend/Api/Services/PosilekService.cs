using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PosilekService : IPosilekService
{
    private readonly ApplicationDbContext _dbContext;

    public PosilekService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<PosilekDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Posilki.AsNoTracking()
            .OrderBy(posilek => posilek.Id)
            .Select(posilek => new PosilekDto
            {
                Id = posilek.Id,
                                Data = posilek.Data,
                                Nazwa = posilek.Nazwa,
                                Opis = posilek.Opis
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<PosilekDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Posilki.AsNoTracking()
            .Where(posilek => posilek.Id == id)
            .Select(posilek => new PosilekDto
            {
                Id = posilek.Id,
                                Data = posilek.Data,
                                Nazwa = posilek.Nazwa,
                                Opis = posilek.Opis
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PosilekDto> CreateAsync(PosilekDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Posilek
        {
            Data = dto.Data,
            Nazwa = dto.Nazwa,
            Opis = dto.Opis,
        };
        _dbContext.Posilki.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, PosilekDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Posilki.FirstOrDefaultAsync(posilek => posilek.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.Data = dto.Data;
        entity.Nazwa = dto.Nazwa;
        entity.Opis = dto.Opis;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Posilki.FirstOrDefaultAsync(posilek => posilek.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.Posilki.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static PosilekDto MapToDto(Posilek entity)
    {
        return new PosilekDto
        {
            Id = entity.Id,
                            Data = entity.Data,
                            Nazwa = entity.Nazwa,
                            Opis = entity.Opis
        };
    }
}
