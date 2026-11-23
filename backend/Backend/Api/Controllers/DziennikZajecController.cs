using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/dzienniki-zajec")]
[Authorize]
public class DziennikZajecController : ControllerBase
{
    private readonly IDziennikZajecService _dziennikZajecService;

    public DziennikZajecController(IDziennikZajecService dziennikZajecService)
    {
        _dziennikZajecService = dziennikZajecService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<DziennikZajecDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _dziennikZajecService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DziennikZajecDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _dziennikZajecService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<DziennikZajecDto>> Create(DziennikZajecDto dto, CancellationToken cancellationToken)
    {
        var created = await _dziennikZajecService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, DziennikZajecDto dto, CancellationToken cancellationToken)
    {
        var updated = await _dziennikZajecService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _dziennikZajecService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
