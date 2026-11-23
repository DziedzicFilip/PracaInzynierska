using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/dziecko-alergen")]
[Authorize]
public class DzieckoAlergenController : ControllerBase
{
    private readonly IDzieckoAlergenService _dzieckoAlergenService;

    public DzieckoAlergenController(IDzieckoAlergenService dzieckoAlergenService)
    {
        _dzieckoAlergenService = dzieckoAlergenService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<DzieckoAlergenDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _dzieckoAlergenService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{dzieckoId:int}/{alergenId:int}")]
    public async Task<ActionResult<DzieckoAlergenDto>> GetById(int dzieckoId, int alergenId, CancellationToken cancellationToken)
    {
        var dto = await _dzieckoAlergenService.GetByIdAsync(dzieckoId, alergenId, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<DzieckoAlergenDto>> Create(DzieckoAlergenDto dto, CancellationToken cancellationToken)
    {
        var created = await _dzieckoAlergenService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { dzieckoId = created.DzieckoId, alergenId = created.AlergenId }, created);
    }

    [HttpPut("{dzieckoId:int}/{alergenId:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int dzieckoId, int alergenId, DzieckoAlergenDto dto, CancellationToken cancellationToken)
    {
        var updated = await _dzieckoAlergenService.UpdateAsync(dzieckoId, alergenId, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{dzieckoId:int}/{alergenId:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int dzieckoId, int alergenId, CancellationToken cancellationToken)
    {
        var deleted = await _dzieckoAlergenService.DeleteAsync(dzieckoId, alergenId, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
