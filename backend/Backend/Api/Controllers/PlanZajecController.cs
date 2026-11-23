using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/plany-zajec")]
[Authorize]
public class PlanZajecController : ControllerBase
{
    private readonly IPlanZajecService _planZajecService;

    public PlanZajecController(IPlanZajecService planZajecService)
    {
        _planZajecService = planZajecService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<PlanZajecDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _planZajecService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PlanZajecDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _planZajecService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<PlanZajecDto>> Create(PlanZajecDto dto, CancellationToken cancellationToken)
    {
        var created = await _planZajecService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, PlanZajecDto dto, CancellationToken cancellationToken)
    {
        var updated = await _planZajecService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _planZajecService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
