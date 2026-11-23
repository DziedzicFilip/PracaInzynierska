using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class IncydentService : IIncydentService
{
    private readonly ApplicationDbContext _dbContext;

    public IncydentService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<IncydentDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Incydenty.AsNoTracking()
            .OrderBy(incydent => incydent.Id)
            .Select(incydent => new IncydentDto
            {
                Id = incydent.Id,
                                DzieckoId = incydent.DzieckoId,
                                RodzicId = incydent.RodzicId,
                                PracownikId = incydent.PracownikId,
                                Powod = incydent.Powod,
                                TypIncydentu = incydent.TypIncydentu,
                                DataZgloszenia = incydent.DataZgloszenia,
                                ProtokolBhP = incydent.ProtokolBhP
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<IncydentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Incydenty.AsNoTracking()
            .Where(incydent => incydent.Id == id)
            .Select(incydent => new IncydentDto
            {
                Id = incydent.Id,
                                DzieckoId = incydent.DzieckoId,
                                RodzicId = incydent.RodzicId,
                                PracownikId = incydent.PracownikId,
                                Powod = incydent.Powod,
                                TypIncydentu = incydent.TypIncydentu,
                                DataZgloszenia = incydent.DataZgloszenia,
                                ProtokolBhP = incydent.ProtokolBhP
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IncydentDto> CreateAsync(IncydentDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Incydent
        {
            DzieckoId = dto.DzieckoId,
            RodzicId = dto.RodzicId,
            PracownikId = dto.PracownikId,
            Powod = dto.Powod,
            TypIncydentu = dto.TypIncydentu,
            DataZgloszenia = dto.DataZgloszenia,
            ProtokolBhP = dto.ProtokolBhP,
        };
        _dbContext.Incydenty.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, IncydentDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Incydenty.FirstOrDefaultAsync(incydent => incydent.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.DzieckoId = dto.DzieckoId;
        entity.RodzicId = dto.RodzicId;
        entity.PracownikId = dto.PracownikId;
        entity.Powod = dto.Powod;
        entity.TypIncydentu = dto.TypIncydentu;
        entity.DataZgloszenia = dto.DataZgloszenia;
        entity.ProtokolBhP = dto.ProtokolBhP;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Incydenty.FirstOrDefaultAsync(incydent => incydent.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.Incydenty.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static IncydentDto MapToDto(Incydent entity)
    {
        return new IncydentDto
        {
            Id = entity.Id,
                            DzieckoId = entity.DzieckoId,
                            RodzicId = entity.RodzicId,
                            PracownikId = entity.PracownikId,
                            Powod = entity.Powod,
                            TypIncydentu = entity.TypIncydentu,
                            DataZgloszenia = entity.DataZgloszenia,
                            ProtokolBhP = entity.ProtokolBhP
        };
    }
}
