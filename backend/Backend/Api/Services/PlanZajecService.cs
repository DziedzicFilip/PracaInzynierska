using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PlanZajecService : IPlanZajecService
{
    private readonly ApplicationDbContext _dbContext;

    public PlanZajecService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<PlanZajecDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.PlanyZajec.AsNoTracking()
            .OrderBy(planZajec => planZajec.Id)
            .Select(planZajec => new PlanZajecDto
            {
                Id = planZajec.Id,
                                ZajecieId = planZajec.ZajecieId,
                                GrupaId = planZajec.GrupaId,
                                SalaId = planZajec.SalaId,
                                PracownikId = planZajec.PracownikId,
                                DzienTygodnia = planZajec.DzienTygodnia,
                                CzasRozpoczecia = planZajec.CzasRozpoczecia,
                                CzasZakonczenia = planZajec.CzasZakonczenia
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<PlanZajecDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.PlanyZajec.AsNoTracking()
            .Where(planZajec => planZajec.Id == id)
            .Select(planZajec => new PlanZajecDto
            {
                Id = planZajec.Id,
                                ZajecieId = planZajec.ZajecieId,
                                GrupaId = planZajec.GrupaId,
                                SalaId = planZajec.SalaId,
                                PracownikId = planZajec.PracownikId,
                                DzienTygodnia = planZajec.DzienTygodnia,
                                CzasRozpoczecia = planZajec.CzasRozpoczecia,
                                CzasZakonczenia = planZajec.CzasZakonczenia
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PlanZajecDto> CreateAsync(PlanZajecDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new PlanZajec
        {
            ZajecieId = dto.ZajecieId,
            GrupaId = dto.GrupaId,
            SalaId = dto.SalaId,
            PracownikId = dto.PracownikId,
            DzienTygodnia = dto.DzienTygodnia,
            CzasRozpoczecia = dto.CzasRozpoczecia,
            CzasZakonczenia = dto.CzasZakonczenia,
        };
        _dbContext.PlanyZajec.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, PlanZajecDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.PlanyZajec.FirstOrDefaultAsync(planZajec => planZajec.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.ZajecieId = dto.ZajecieId;
        entity.GrupaId = dto.GrupaId;
        entity.SalaId = dto.SalaId;
        entity.PracownikId = dto.PracownikId;
        entity.DzienTygodnia = dto.DzienTygodnia;
        entity.CzasRozpoczecia = dto.CzasRozpoczecia;
        entity.CzasZakonczenia = dto.CzasZakonczenia;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.PlanyZajec.FirstOrDefaultAsync(planZajec => planZajec.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.PlanyZajec.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static PlanZajecDto MapToDto(PlanZajec entity)
    {
        return new PlanZajecDto
        {
            Id = entity.Id,
                            ZajecieId = entity.ZajecieId,
                            GrupaId = entity.GrupaId,
                            SalaId = entity.SalaId,
                            PracownikId = entity.PracownikId,
                            DzienTygodnia = entity.DzienTygodnia,
                            CzasRozpoczecia = entity.CzasRozpoczecia,
                            CzasZakonczenia = entity.CzasZakonczenia
        };
    }
}
