using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/plany-dnia-grupy")]
[Authorize]
public class PlanDniaGrupyController : ControllerBase
{
    private readonly IPlanDniaGrupyService _planDniaGrupyService;

    public PlanDniaGrupyController(IPlanDniaGrupyService planDniaGrupyService)
    {
        _planDniaGrupyService = planDniaGrupyService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<PlanDniaGrupyDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _planDniaGrupyService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PlanDniaGrupyDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _planDniaGrupyService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<PlanDniaGrupyDto>> Create(PlanDniaGrupyDto dto, CancellationToken cancellationToken)
    {
        var created = await _planDniaGrupyService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, PlanDniaGrupyDto dto, CancellationToken cancellationToken)
    {
        var updated = await _planDniaGrupyService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _planDniaGrupyService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
