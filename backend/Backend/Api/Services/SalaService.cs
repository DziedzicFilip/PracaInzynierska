using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class SalaService : ISalaService
{
    private readonly ApplicationDbContext _dbContext;

    public SalaService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<SalaDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Sale
            .AsNoTracking()
            .OrderBy(sala => sala.Id)
            .Select(sala => new SalaDto
            {
                Id = sala.Id,
                Nazwa = sala.Nazwa,
                TypSali = sala.TypSali
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<SalaDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Sale
            .AsNoTracking()
            .Select(sala => new SalaDto
            {
                Id = sala.Id,
                Nazwa = sala.Nazwa,
                TypSali = sala.TypSali
            })
            .FirstOrDefaultAsync(sala => sala.Id == id, cancellationToken);
    }

    public async Task<SalaDto> CreateAsync(
        SalaDto dto,
        CancellationToken cancellationToken = default)
    {
        var sala = new Sala
        {
            Nazwa = dto.Nazwa,
            TypSali = dto.TypSali
        };

        _dbContext.Sale.Add(sala);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new SalaDto
        {
            Id = sala.Id,
            Nazwa = sala.Nazwa,
            TypSali = sala.TypSali
        };
    }

    public async Task<bool> UpdateAsync(
        int id,
        SalaDto dto,
        CancellationToken cancellationToken = default)
    {
        var existingSala = await _dbContext.Sale
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);

        if (existingSala is null)
        {
            return false;
        }

        existingSala.Nazwa = dto.Nazwa;
        existingSala.TypSali = dto.TypSali;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var sala = await _dbContext.Sale
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);

        if (sala is null)
        {
            return false;
        }

        _dbContext.Sale.Remove(sala);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
