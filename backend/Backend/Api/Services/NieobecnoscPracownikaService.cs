using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class NieobecnoscPracownikaService : INieobecnoscPracownikaService
{
    private readonly ApplicationDbContext _dbContext;

    public NieobecnoscPracownikaService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<NieobecnoscPracownikaDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.NieobecnosciPracownikow.AsNoTracking()
            .OrderBy(nieobecnoscPracownika => nieobecnoscPracownika.Id)
            .Select(nieobecnoscPracownika => new NieobecnoscPracownikaDto
            {
                Id = nieobecnoscPracownika.Id,
                                PracownikId = nieobecnoscPracownika.PracownikId,
                                ZastepcaId = nieobecnoscPracownika.ZastepcaId,
                                Powod = nieobecnoscPracownika.Powod,
                                DataOd = nieobecnoscPracownika.DataOd,
                                DataDo = nieobecnoscPracownika.DataDo
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<NieobecnoscPracownikaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.NieobecnosciPracownikow.AsNoTracking()
            .Where(nieobecnoscPracownika => nieobecnoscPracownika.Id == id)
            .Select(nieobecnoscPracownika => new NieobecnoscPracownikaDto
            {
                Id = nieobecnoscPracownika.Id,
                                PracownikId = nieobecnoscPracownika.PracownikId,
                                ZastepcaId = nieobecnoscPracownika.ZastepcaId,
                                Powod = nieobecnoscPracownika.Powod,
                                DataOd = nieobecnoscPracownika.DataOd,
                                DataDo = nieobecnoscPracownika.DataDo
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<NieobecnoscPracownikaDto> CreateAsync(NieobecnoscPracownikaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new NieobecnoscPracownika
        {
            PracownikId = dto.PracownikId,
            ZastepcaId = dto.ZastepcaId,
            Powod = dto.Powod,
            DataOd = dto.DataOd,
            DataDo = dto.DataDo,
        };
        _dbContext.NieobecnosciPracownikow.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, NieobecnoscPracownikaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.NieobecnosciPracownikow.FirstOrDefaultAsync(nieobecnoscPracownika => nieobecnoscPracownika.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.PracownikId = dto.PracownikId;
        entity.ZastepcaId = dto.ZastepcaId;
        entity.Powod = dto.Powod;
        entity.DataOd = dto.DataOd;
        entity.DataDo = dto.DataDo;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.NieobecnosciPracownikow.FirstOrDefaultAsync(nieobecnoscPracownika => nieobecnoscPracownika.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.NieobecnosciPracownikow.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static NieobecnoscPracownikaDto MapToDto(NieobecnoscPracownika entity)
    {
        return new NieobecnoscPracownikaDto
        {
            Id = entity.Id,
                            PracownikId = entity.PracownikId,
                            ZastepcaId = entity.ZastepcaId,
                            Powod = entity.Powod,
                            DataOd = entity.DataOd,
                            DataDo = entity.DataDo
        };
    }
}
