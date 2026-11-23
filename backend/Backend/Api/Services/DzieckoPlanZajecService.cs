using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class DzieckoPlanZajecService : IDzieckoPlanZajecService
{
    private readonly ApplicationDbContext _dbContext;

    public DzieckoPlanZajecService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<DzieckoPlanZajecDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.DzieckoPlanyZajec.AsNoTracking()
            .OrderBy(dzieckoPlanZajec => dzieckoPlanZajec.DzieckoId)
            .Select(dzieckoPlanZajec => new DzieckoPlanZajecDto
            {
                DzieckoId = dzieckoPlanZajec.DzieckoId,
                                PlanZajecId = dzieckoPlanZajec.PlanZajecId
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<DzieckoPlanZajecDto?> GetByIdAsync(int dzieckoId, int planZajecId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.DzieckoPlanyZajec.AsNoTracking()
            .Where(dzieckoPlanZajec => dzieckoPlanZajec.DzieckoId == dzieckoId && dzieckoPlanZajec.PlanZajecId == planZajecId)
            .Select(dzieckoPlanZajec => new DzieckoPlanZajecDto
            {
                DzieckoId = dzieckoPlanZajec.DzieckoId,
                                PlanZajecId = dzieckoPlanZajec.PlanZajecId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<DzieckoPlanZajecDto> CreateAsync(DzieckoPlanZajecDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new DzieckoPlanZajec
        {
            DzieckoId = dto.DzieckoId,
            PlanZajecId = dto.PlanZajecId,
        };
        _dbContext.DzieckoPlanyZajec.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int dzieckoId, int planZajecId, DzieckoPlanZajecDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.DzieckoPlanyZajec.FirstOrDefaultAsync(dzieckoPlanZajec => dzieckoPlanZajec.DzieckoId == dzieckoId && dzieckoPlanZajec.PlanZajecId == planZajecId, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int dzieckoId, int planZajecId, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.DzieckoPlanyZajec.FirstOrDefaultAsync(dzieckoPlanZajec => dzieckoPlanZajec.DzieckoId == dzieckoId && dzieckoPlanZajec.PlanZajecId == planZajecId, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.DzieckoPlanyZajec.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static DzieckoPlanZajecDto MapToDto(DzieckoPlanZajec entity)
    {
        return new DzieckoPlanZajecDto
        {
            DzieckoId = entity.DzieckoId,
                            PlanZajecId = entity.PlanZajecId
        };
    }
}
