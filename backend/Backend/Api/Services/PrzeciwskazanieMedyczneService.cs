using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PrzeciwskazanieMedyczneService : IPrzeciwskazanieMedyczneService
{
    private readonly ApplicationDbContext _dbContext;

    public PrzeciwskazanieMedyczneService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<PrzeciwskazanieMedyczneDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.PrzeciwwskazaniaMedyczne.AsNoTracking()
            .OrderBy(przeciwskazanieMedyczne => przeciwskazanieMedyczne.Id)
            .Select(przeciwskazanieMedyczne => new PrzeciwskazanieMedyczneDto
            {
                Id = przeciwskazanieMedyczne.Id,
                                DzieckoId = przeciwskazanieMedyczne.DzieckoId,
                                Opis = przeciwskazanieMedyczne.Opis
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<PrzeciwskazanieMedyczneDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.PrzeciwwskazaniaMedyczne.AsNoTracking()
            .Where(przeciwskazanieMedyczne => przeciwskazanieMedyczne.Id == id)
            .Select(przeciwskazanieMedyczne => new PrzeciwskazanieMedyczneDto
            {
                Id = przeciwskazanieMedyczne.Id,
                                DzieckoId = przeciwskazanieMedyczne.DzieckoId,
                                Opis = przeciwskazanieMedyczne.Opis
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PrzeciwskazanieMedyczneDto> CreateAsync(PrzeciwskazanieMedyczneDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new PrzeciwskazanieMedyczne
        {
            DzieckoId = dto.DzieckoId,
            Opis = dto.Opis,
        };
        _dbContext.PrzeciwwskazaniaMedyczne.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, PrzeciwskazanieMedyczneDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.PrzeciwwskazaniaMedyczne.FirstOrDefaultAsync(przeciwskazanieMedyczne => przeciwskazanieMedyczne.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.DzieckoId = dto.DzieckoId;
        entity.Opis = dto.Opis;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.PrzeciwwskazaniaMedyczne.FirstOrDefaultAsync(przeciwskazanieMedyczne => przeciwskazanieMedyczne.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.PrzeciwwskazaniaMedyczne.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static PrzeciwskazanieMedyczneDto MapToDto(PrzeciwskazanieMedyczne entity)
    {
        return new PrzeciwskazanieMedyczneDto
        {
            Id = entity.Id,
                            DzieckoId = entity.DzieckoId,
                            Opis = entity.Opis
        };
    }
}
