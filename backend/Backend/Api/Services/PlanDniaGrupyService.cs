using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PlanDniaGrupyService : IPlanDniaGrupyService
{
    private readonly ApplicationDbContext _dbContext;

    public PlanDniaGrupyService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<PlanDniaGrupyDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.PlanyDniaGrupy.AsNoTracking()
            .OrderBy(planDniaGrupy => planDniaGrupy.Id)
            .Select(planDniaGrupy => new PlanDniaGrupyDto
            {
                Id = planDniaGrupy.Id,
                                GrupaId = planDniaGrupy.GrupaId,
                                GodzinaOd = planDniaGrupy.GodzinaOd,
                                GodzinaDo = planDniaGrupy.GodzinaDo,
                                Czynnosc = planDniaGrupy.Czynnosc
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<PlanDniaGrupyDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.PlanyDniaGrupy.AsNoTracking()
            .Where(planDniaGrupy => planDniaGrupy.Id == id)
            .Select(planDniaGrupy => new PlanDniaGrupyDto
            {
                Id = planDniaGrupy.Id,
                                GrupaId = planDniaGrupy.GrupaId,
                                GodzinaOd = planDniaGrupy.GodzinaOd,
                                GodzinaDo = planDniaGrupy.GodzinaDo,
                                Czynnosc = planDniaGrupy.Czynnosc
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PlanDniaGrupyDto> CreateAsync(PlanDniaGrupyDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new PlanDniaGrupy
        {
            GrupaId = dto.GrupaId,
            GodzinaOd = dto.GodzinaOd,
            GodzinaDo = dto.GodzinaDo,
            Czynnosc = dto.Czynnosc,
        };
        _dbContext.PlanyDniaGrupy.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, PlanDniaGrupyDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.PlanyDniaGrupy.FirstOrDefaultAsync(planDniaGrupy => planDniaGrupy.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.GrupaId = dto.GrupaId;
        entity.GodzinaOd = dto.GodzinaOd;
        entity.GodzinaDo = dto.GodzinaDo;
        entity.Czynnosc = dto.Czynnosc;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.PlanyDniaGrupy.FirstOrDefaultAsync(planDniaGrupy => planDniaGrupy.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.PlanyDniaGrupy.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static PlanDniaGrupyDto MapToDto(PlanDniaGrupy entity)
    {
        return new PlanDniaGrupyDto
        {
            Id = entity.Id,
                            GrupaId = entity.GrupaId,
                            GodzinaOd = entity.GodzinaOd,
                            GodzinaDo = entity.GodzinaDo,
                            Czynnosc = entity.Czynnosc
        };
    }
}
