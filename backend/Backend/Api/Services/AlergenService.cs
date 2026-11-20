using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class AlergenService : IAlergenService
{
    private readonly ApplicationDbContext _dbContext;

    public AlergenService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<AlergenDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Alergeny
            .AsNoTracking()
            .OrderBy(alergen => alergen.Id)
            .Select(alergen => new AlergenDto
            {
                Id = alergen.Id,
                Nazwa = alergen.Nazwa
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<AlergenDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Alergeny
            .AsNoTracking()
            .Select(alergen => new AlergenDto
            {
                Id = alergen.Id,
                Nazwa = alergen.Nazwa
            })
            .FirstOrDefaultAsync(alergen => alergen.Id == id, cancellationToken);
    }

    public async Task<AlergenDto> CreateAsync(
        AlergenDto dto,
        CancellationToken cancellationToken = default)
    {
        var alergen = new Alergen
        {
            Nazwa = dto.Nazwa
        };

        _dbContext.Alergeny.Add(alergen);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AlergenDto
        {
            Id = alergen.Id,
            Nazwa = alergen.Nazwa
        };
    }

    public async Task<bool> UpdateAsync(
        int id,
        AlergenDto dto,
        CancellationToken cancellationToken = default)
    {
        var existingAlergen = await _dbContext.Alergeny
            .FirstOrDefaultAsync(alergen => alergen.Id == id, cancellationToken);

        if (existingAlergen is null)
        {
            return false;
        }

        existingAlergen.Nazwa = dto.Nazwa;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var alergen = await _dbContext.Alergeny
            .FirstOrDefaultAsync(alergen => alergen.Id == id, cancellationToken);

        if (alergen is null)
        {
            return false;
        }

        _dbContext.Alergeny.Remove(alergen);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
