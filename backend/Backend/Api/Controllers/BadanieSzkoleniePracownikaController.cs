using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/badania-szkolenia-pracownikow")]
[Authorize]
public class BadanieSzkoleniePracownikaController : ControllerBase
{
    private readonly IBadanieSzkoleniePracownikaService _badanieSzkoleniePracownikaService;

    public BadanieSzkoleniePracownikaController(IBadanieSzkoleniePracownikaService badanieSzkoleniePracownikaService)
    {
        _badanieSzkoleniePracownikaService = badanieSzkoleniePracownikaService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<BadanieSzkoleniePracownikaDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _badanieSzkoleniePracownikaService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BadanieSzkoleniePracownikaDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _badanieSzkoleniePracownikaService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<BadanieSzkoleniePracownikaDto>> Create(BadanieSzkoleniePracownikaDto dto, CancellationToken cancellationToken)
    {
        var created = await _badanieSzkoleniePracownikaService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, BadanieSzkoleniePracownikaDto dto, CancellationToken cancellationToken)
    {
        var updated = await _badanieSzkoleniePracownikaService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _badanieSzkoleniePracownikaService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
