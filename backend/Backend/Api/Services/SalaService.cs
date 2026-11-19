using Api.Data;
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

    public async Task<IReadOnlyCollection<Sala>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Sale
            .AsNoTracking()
            .OrderBy(sala => sala.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Sala?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Sale
            .AsNoTracking()
            .FirstOrDefaultAsync(sala => sala.Id == id, cancellationToken);
    }

    public async Task<Sala> CreateAsync(
        Sala sala,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Sale.Add(sala);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return sala;
    }

    public async Task<bool> UpdateAsync(
        int id,
        Sala sala,
        CancellationToken cancellationToken = default)
    {
        var existingSala = await _dbContext.Sale
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);

        if (existingSala is null)
        {
            return false;
        }

        existingSala.Nazwa = sala.Nazwa;
        existingSala.TypSali = sala.TypSali;

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
