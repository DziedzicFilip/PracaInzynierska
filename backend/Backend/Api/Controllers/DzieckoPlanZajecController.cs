using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/dziecko-plan-zajec")]
[Authorize]
public class DzieckoPlanZajecController : ControllerBase
{
    private readonly IDzieckoPlanZajecService _dzieckoPlanZajecService;

    public DzieckoPlanZajecController(IDzieckoPlanZajecService dzieckoPlanZajecService)
    {
        _dzieckoPlanZajecService = dzieckoPlanZajecService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<DzieckoPlanZajecDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _dzieckoPlanZajecService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{dzieckoId:int}/{planZajecId:int}")]
    public async Task<ActionResult<DzieckoPlanZajecDto>> GetById(int dzieckoId, int planZajecId, CancellationToken cancellationToken)
    {
        var dto = await _dzieckoPlanZajecService.GetByIdAsync(dzieckoId, planZajecId, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<DzieckoPlanZajecDto>> Create(DzieckoPlanZajecDto dto, CancellationToken cancellationToken)
    {
        var created = await _dzieckoPlanZajecService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { dzieckoId = created.DzieckoId, planZajecId = created.PlanZajecId }, created);
    }

    [HttpPut("{dzieckoId:int}/{planZajecId:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int dzieckoId, int planZajecId, DzieckoPlanZajecDto dto, CancellationToken cancellationToken)
    {
        var updated = await _dzieckoPlanZajecService.UpdateAsync(dzieckoId, planZajecId, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{dzieckoId:int}/{planZajecId:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int dzieckoId, int planZajecId, CancellationToken cancellationToken)
    {
        var deleted = await _dzieckoPlanZajecService.DeleteAsync(dzieckoId, planZajecId, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
