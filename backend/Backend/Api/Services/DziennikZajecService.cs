using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class DziennikZajecService : IDziennikZajecService
{
    private readonly ApplicationDbContext _dbContext;

    public DziennikZajecService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<DziennikZajecDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.DziennikiZajec.AsNoTracking()
            .OrderBy(dziennikZajec => dziennikZajec.Id)
            .Select(dziennikZajec => new DziennikZajecDto
            {
                Id = dziennikZajec.Id,
                                GrupaId = dziennikZajec.GrupaId,
                                PracownikId = dziennikZajec.PracownikId,
                                Data = dziennikZajec.Data,
                                Temat = dziennikZajec.Temat,
                                PodstawaProgramowa = dziennikZajec.PodstawaProgramowa
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<DziennikZajecDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.DziennikiZajec.AsNoTracking()
            .Where(dziennikZajec => dziennikZajec.Id == id)
            .Select(dziennikZajec => new DziennikZajecDto
            {
                Id = dziennikZajec.Id,
                                GrupaId = dziennikZajec.GrupaId,
                                PracownikId = dziennikZajec.PracownikId,
                                Data = dziennikZajec.Data,
                                Temat = dziennikZajec.Temat,
                                PodstawaProgramowa = dziennikZajec.PodstawaProgramowa
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<DziennikZajecDto> CreateAsync(DziennikZajecDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new DziennikZajec
        {
            GrupaId = dto.GrupaId,
            PracownikId = dto.PracownikId,
            Data = dto.Data,
            Temat = dto.Temat,
            PodstawaProgramowa = dto.PodstawaProgramowa,
        };
        _dbContext.DziennikiZajec.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, DziennikZajecDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.DziennikiZajec.FirstOrDefaultAsync(dziennikZajec => dziennikZajec.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.GrupaId = dto.GrupaId;
        entity.PracownikId = dto.PracownikId;
        entity.Data = dto.Data;
        entity.Temat = dto.Temat;
        entity.PodstawaProgramowa = dto.PodstawaProgramowa;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.DziennikiZajec.FirstOrDefaultAsync(dziennikZajec => dziennikZajec.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.DziennikiZajec.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static DziennikZajecDto MapToDto(DziennikZajec entity)
    {
        return new DziennikZajecDto
        {
            Id = entity.Id,
                            GrupaId = entity.GrupaId,
                            PracownikId = entity.PracownikId,
                            Data = entity.Data,
                            Temat = entity.Temat,
                            PodstawaProgramowa = entity.PodstawaProgramowa
        };
    }
}
