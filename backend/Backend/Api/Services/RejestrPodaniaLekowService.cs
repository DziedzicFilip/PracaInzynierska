using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class RejestrPodaniaLekowService : IRejestrPodaniaLekowService
{
    private readonly ApplicationDbContext _dbContext;

    public RejestrPodaniaLekowService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<RejestrPodaniaLekowDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.RejestryPodaniaLekow.AsNoTracking()
            .OrderBy(rejestrPodaniaLekow => rejestrPodaniaLekow.Id)
            .Select(rejestrPodaniaLekow => new RejestrPodaniaLekowDto
            {
                Id = rejestrPodaniaLekow.Id,
                                LekId = rejestrPodaniaLekow.LekId,
                                PracownikId = rejestrPodaniaLekow.PracownikId,
                                DataPodania = rejestrPodaniaLekow.DataPodania,
                                PotwierdzeniePodpisu = rejestrPodaniaLekow.PotwierdzeniePodpisu
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<RejestrPodaniaLekowDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.RejestryPodaniaLekow.AsNoTracking()
            .Where(rejestrPodaniaLekow => rejestrPodaniaLekow.Id == id)
            .Select(rejestrPodaniaLekow => new RejestrPodaniaLekowDto
            {
                Id = rejestrPodaniaLekow.Id,
                                LekId = rejestrPodaniaLekow.LekId,
                                PracownikId = rejestrPodaniaLekow.PracownikId,
                                DataPodania = rejestrPodaniaLekow.DataPodania,
                                PotwierdzeniePodpisu = rejestrPodaniaLekow.PotwierdzeniePodpisu
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<RejestrPodaniaLekowDto> CreateAsync(RejestrPodaniaLekowDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new RejestrPodaniaLekow
        {
            LekId = dto.LekId,
            PracownikId = dto.PracownikId,
            DataPodania = dto.DataPodania,
            PotwierdzeniePodpisu = dto.PotwierdzeniePodpisu,
        };
        _dbContext.RejestryPodaniaLekow.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, RejestrPodaniaLekowDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.RejestryPodaniaLekow.FirstOrDefaultAsync(rejestrPodaniaLekow => rejestrPodaniaLekow.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.LekId = dto.LekId;
        entity.PracownikId = dto.PracownikId;
        entity.DataPodania = dto.DataPodania;
        entity.PotwierdzeniePodpisu = dto.PotwierdzeniePodpisu;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.RejestryPodaniaLekow.FirstOrDefaultAsync(rejestrPodaniaLekow => rejestrPodaniaLekow.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.RejestryPodaniaLekow.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static RejestrPodaniaLekowDto MapToDto(RejestrPodaniaLekow entity)
    {
        return new RejestrPodaniaLekowDto
        {
            Id = entity.Id,
                            LekId = entity.LekId,
                            PracownikId = entity.PracownikId,
                            DataPodania = entity.DataPodania,
                            PotwierdzeniePodpisu = entity.PotwierdzeniePodpisu
        };
    }
}
