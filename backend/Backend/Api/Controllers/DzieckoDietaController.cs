using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/dziecko-dieta")]
[Authorize]
public class DzieckoDietaController : ControllerBase
{
    private readonly IDzieckoDietaService _dzieckoDietaService;

    public DzieckoDietaController(IDzieckoDietaService dzieckoDietaService)
    {
        _dzieckoDietaService = dzieckoDietaService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<DzieckoDietaDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _dzieckoDietaService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{dzieckoId:int}/{dietaId:int}")]
    public async Task<ActionResult<DzieckoDietaDto>> GetById(int dzieckoId, int dietaId, CancellationToken cancellationToken)
    {
        var dto = await _dzieckoDietaService.GetByIdAsync(dzieckoId, dietaId, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<DzieckoDietaDto>> Create(DzieckoDietaDto dto, CancellationToken cancellationToken)
    {
        var created = await _dzieckoDietaService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { dzieckoId = created.DzieckoId, dietaId = created.DietaId }, created);
    }

    [HttpPut("{dzieckoId:int}/{dietaId:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int dzieckoId, int dietaId, DzieckoDietaDto dto, CancellationToken cancellationToken)
    {
        var updated = await _dzieckoDietaService.UpdateAsync(dzieckoId, dietaId, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{dzieckoId:int}/{dietaId:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int dzieckoId, int dietaId, CancellationToken cancellationToken)
    {
        var deleted = await _dzieckoDietaService.DeleteAsync(dzieckoId, dietaId, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
