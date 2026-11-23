using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class UsterkaPomocyService : IUsterkaPomocyService
{
    private readonly ApplicationDbContext _dbContext;

    public UsterkaPomocyService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<UsterkaPomocyDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.UsterkiPomocyDydaktycznych.AsNoTracking()
            .OrderBy(usterkaPomocy => usterkaPomocy.Id)
            .Select(usterkaPomocy => new UsterkaPomocyDto
            {
                Id = usterkaPomocy.Id,
                                PomocId = usterkaPomocy.PomocId,
                                PracownikZglaszajacyId = usterkaPomocy.PracownikZglaszajacyId,
                                Opis = usterkaPomocy.Opis,
                                DataZgloszenia = usterkaPomocy.DataZgloszenia,
                                Status = usterkaPomocy.Status
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<UsterkaPomocyDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UsterkiPomocyDydaktycznych.AsNoTracking()
            .Where(usterkaPomocy => usterkaPomocy.Id == id)
            .Select(usterkaPomocy => new UsterkaPomocyDto
            {
                Id = usterkaPomocy.Id,
                                PomocId = usterkaPomocy.PomocId,
                                PracownikZglaszajacyId = usterkaPomocy.PracownikZglaszajacyId,
                                Opis = usterkaPomocy.Opis,
                                DataZgloszenia = usterkaPomocy.DataZgloszenia,
                                Status = usterkaPomocy.Status
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<UsterkaPomocyDto> CreateAsync(UsterkaPomocyDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new UsterkaPomocy
        {
            PomocId = dto.PomocId,
            PracownikZglaszajacyId = dto.PracownikZglaszajacyId,
            Opis = dto.Opis,
            DataZgloszenia = dto.DataZgloszenia,
            Status = dto.Status,
        };
        _dbContext.UsterkiPomocyDydaktycznych.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, UsterkaPomocyDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.UsterkiPomocyDydaktycznych.FirstOrDefaultAsync(usterkaPomocy => usterkaPomocy.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.PomocId = dto.PomocId;
        entity.PracownikZglaszajacyId = dto.PracownikZglaszajacyId;
        entity.Opis = dto.Opis;
        entity.DataZgloszenia = dto.DataZgloszenia;
        entity.Status = dto.Status;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.UsterkiPomocyDydaktycznych.FirstOrDefaultAsync(usterkaPomocy => usterkaPomocy.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.UsterkiPomocyDydaktycznych.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static UsterkaPomocyDto MapToDto(UsterkaPomocy entity)
    {
        return new UsterkaPomocyDto
        {
            Id = entity.Id,
                            PomocId = entity.PomocId,
                            PracownikZglaszajacyId = entity.PracownikZglaszajacyId,
                            Opis = entity.Opis,
                            DataZgloszenia = entity.DataZgloszenia,
                            Status = entity.Status
        };
    }
}
