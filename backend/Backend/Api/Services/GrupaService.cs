using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class GrupaService : IGrupaService
{
    private readonly ApplicationDbContext _dbContext;

    public GrupaService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<GrupaDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Grupy.AsNoTracking()
            .OrderBy(grupa => grupa.Id)
            .Select(grupa => new GrupaDto
            {
                Id = grupa.Id,
                                Nazwa = grupa.Nazwa,
                                WiekDzieci = grupa.WiekDzieci,
                                WychowawcaId = grupa.WychowawcaId,
                                SalaId = grupa.SalaId
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<GrupaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Grupy.AsNoTracking()
            .Where(grupa => grupa.Id == id)
            .Select(grupa => new GrupaDto
            {
                Id = grupa.Id,
                                Nazwa = grupa.Nazwa,
                                WiekDzieci = grupa.WiekDzieci,
                                WychowawcaId = grupa.WychowawcaId,
                                SalaId = grupa.SalaId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<GrupaDto> CreateAsync(GrupaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Grupa
        {
            Nazwa = dto.Nazwa,
            WiekDzieci = dto.WiekDzieci,
            WychowawcaId = dto.WychowawcaId,
            SalaId = dto.SalaId,
        };
        _dbContext.Grupy.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, GrupaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Grupy.FirstOrDefaultAsync(grupa => grupa.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.Nazwa = dto.Nazwa;
        entity.WiekDzieci = dto.WiekDzieci;
        entity.WychowawcaId = dto.WychowawcaId;
        entity.SalaId = dto.SalaId;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Grupy.FirstOrDefaultAsync(grupa => grupa.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.Grupy.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static GrupaDto MapToDto(Grupa entity)
    {
        return new GrupaDto
        {
            Id = entity.Id,
                            Nazwa = entity.Nazwa,
                            WiekDzieci = entity.WiekDzieci,
                            WychowawcaId = entity.WychowawcaId,
                            SalaId = entity.SalaId
        };
    }
}
