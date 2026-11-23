using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class NadgodzinyPracownikaService : INadgodzinyPracownikaService
{
    private readonly ApplicationDbContext _dbContext;

    public NadgodzinyPracownikaService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<NadgodzinyPracownikaDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.NadgodzinyPracownikow.AsNoTracking()
            .OrderBy(nadgodzinyPracownika => nadgodzinyPracownika.Id)
            .Select(nadgodzinyPracownika => new NadgodzinyPracownikaDto
            {
                Id = nadgodzinyPracownika.Id,
                                PracownikId = nadgodzinyPracownika.PracownikId,
                                Data = nadgodzinyPracownika.Data,
                                IloscGodzin = nadgodzinyPracownika.IloscGodzin,
                                Powod = nadgodzinyPracownika.Powod
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<NadgodzinyPracownikaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.NadgodzinyPracownikow.AsNoTracking()
            .Where(nadgodzinyPracownika => nadgodzinyPracownika.Id == id)
            .Select(nadgodzinyPracownika => new NadgodzinyPracownikaDto
            {
                Id = nadgodzinyPracownika.Id,
                                PracownikId = nadgodzinyPracownika.PracownikId,
                                Data = nadgodzinyPracownika.Data,
                                IloscGodzin = nadgodzinyPracownika.IloscGodzin,
                                Powod = nadgodzinyPracownika.Powod
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<NadgodzinyPracownikaDto> CreateAsync(NadgodzinyPracownikaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new NadgodzinyPracownika
        {
            PracownikId = dto.PracownikId,
            Data = dto.Data,
            IloscGodzin = dto.IloscGodzin,
            Powod = dto.Powod,
        };
        _dbContext.NadgodzinyPracownikow.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, NadgodzinyPracownikaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.NadgodzinyPracownikow.FirstOrDefaultAsync(nadgodzinyPracownika => nadgodzinyPracownika.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.PracownikId = dto.PracownikId;
        entity.Data = dto.Data;
        entity.IloscGodzin = dto.IloscGodzin;
        entity.Powod = dto.Powod;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.NadgodzinyPracownikow.FirstOrDefaultAsync(nadgodzinyPracownika => nadgodzinyPracownika.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.NadgodzinyPracownikow.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static NadgodzinyPracownikaDto MapToDto(NadgodzinyPracownika entity)
    {
        return new NadgodzinyPracownikaDto
        {
            Id = entity.Id,
                            PracownikId = entity.PracownikId,
                            Data = entity.Data,
                            IloscGodzin = entity.IloscGodzin,
                            Powod = entity.Powod
        };
    }
}
