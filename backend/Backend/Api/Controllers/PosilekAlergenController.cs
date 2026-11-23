using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/posilek-alergen")]
[Authorize]
public class PosilekAlergenController : ControllerBase
{
    private readonly IPosilekAlergenService _posilekAlergenService;

    public PosilekAlergenController(IPosilekAlergenService posilekAlergenService)
    {
        _posilekAlergenService = posilekAlergenService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<PosilekAlergenDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _posilekAlergenService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{posilekId:int}/{alergenId:int}")]
    public async Task<ActionResult<PosilekAlergenDto>> GetById(int posilekId, int alergenId, CancellationToken cancellationToken)
    {
        var dto = await _posilekAlergenService.GetByIdAsync(posilekId, alergenId, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<PosilekAlergenDto>> Create(PosilekAlergenDto dto, CancellationToken cancellationToken)
    {
        var created = await _posilekAlergenService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { posilekId = created.PosilekId, alergenId = created.AlergenId }, created);
    }

    [HttpPut("{posilekId:int}/{alergenId:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int posilekId, int alergenId, PosilekAlergenDto dto, CancellationToken cancellationToken)
    {
        var updated = await _posilekAlergenService.UpdateAsync(posilekId, alergenId, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{posilekId:int}/{alergenId:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int posilekId, int alergenId, CancellationToken cancellationToken)
    {
        var deleted = await _posilekAlergenService.DeleteAsync(posilekId, alergenId, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
