using Api.Data;
using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class WydarzenieKalendarzService : IWydarzenieKalendarzService
{
    private readonly ApplicationDbContext _dbContext;

    public WydarzenieKalendarzService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<WydarzenieKalendarzDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.WydarzeniaKalendarza.AsNoTracking()
            .OrderBy(wydarzenieKalendarz => wydarzenieKalendarz.Id)
            .Select(wydarzenieKalendarz => new WydarzenieKalendarzDto
            {
                Id = wydarzenieKalendarz.Id,
                                Nazwa = wydarzenieKalendarz.Nazwa,
                                Opis = wydarzenieKalendarz.Opis,
                                DataWydarzenia = wydarzenieKalendarz.DataWydarzenia,
                                GodzinaRozpoczecia = wydarzenieKalendarz.GodzinaRozpoczecia,
                                GodzinaZakonczenia = wydarzenieKalendarz.GodzinaZakonczenia,
                                PracownikOdpowiedzialnyId = wydarzenieKalendarz.PracownikOdpowiedzialnyId
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<WydarzenieKalendarzDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.WydarzeniaKalendarza.AsNoTracking()
            .Where(wydarzenieKalendarz => wydarzenieKalendarz.Id == id)
            .Select(wydarzenieKalendarz => new WydarzenieKalendarzDto
            {
                Id = wydarzenieKalendarz.Id,
                                Nazwa = wydarzenieKalendarz.Nazwa,
                                Opis = wydarzenieKalendarz.Opis,
                                DataWydarzenia = wydarzenieKalendarz.DataWydarzenia,
                                GodzinaRozpoczecia = wydarzenieKalendarz.GodzinaRozpoczecia,
                                GodzinaZakonczenia = wydarzenieKalendarz.GodzinaZakonczenia,
                                PracownikOdpowiedzialnyId = wydarzenieKalendarz.PracownikOdpowiedzialnyId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<WydarzenieKalendarzDto> CreateAsync(WydarzenieKalendarzDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new WydarzenieKalendarz
        {
            Nazwa = dto.Nazwa,
            Opis = dto.Opis,
            DataWydarzenia = dto.DataWydarzenia,
            GodzinaRozpoczecia = dto.GodzinaRozpoczecia,
            GodzinaZakonczenia = dto.GodzinaZakonczenia,
            PracownikOdpowiedzialnyId = dto.PracownikOdpowiedzialnyId,
        };
        _dbContext.WydarzeniaKalendarza.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, WydarzenieKalendarzDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.WydarzeniaKalendarza.FirstOrDefaultAsync(wydarzenieKalendarz => wydarzenieKalendarz.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.Nazwa = dto.Nazwa;
        entity.Opis = dto.Opis;
        entity.DataWydarzenia = dto.DataWydarzenia;
        entity.GodzinaRozpoczecia = dto.GodzinaRozpoczecia;
        entity.GodzinaZakonczenia = dto.GodzinaZakonczenia;
        entity.PracownikOdpowiedzialnyId = dto.PracownikOdpowiedzialnyId;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.WydarzeniaKalendarza.FirstOrDefaultAsync(wydarzenieKalendarz => wydarzenieKalendarz.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.WydarzeniaKalendarza.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static WydarzenieKalendarzDto MapToDto(WydarzenieKalendarz entity)
    {
        return new WydarzenieKalendarzDto
        {
            Id = entity.Id,
                            Nazwa = entity.Nazwa,
                            Opis = entity.Opis,
                            DataWydarzenia = entity.DataWydarzenia,
                            GodzinaRozpoczecia = entity.GodzinaRozpoczecia,
                            GodzinaZakonczenia = entity.GodzinaZakonczenia,
                            PracownikOdpowiedzialnyId = entity.PracownikOdpowiedzialnyId
        };
    }
}
