using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/grafik-pracy")]
[Authorize]
public class GrafikPracyController : ControllerBase
{
    private readonly IGrafikPracyService _grafikPracyService;

    public GrafikPracyController(IGrafikPracyService grafikPracyService)
    {
        _grafikPracyService = grafikPracyService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<GrafikPracyDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _grafikPracyService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GrafikPracyDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _grafikPracyService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<GrafikPracyDto>> Create(GrafikPracyDto dto, CancellationToken cancellationToken)
    {
        var created = await _grafikPracyService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, GrafikPracyDto dto, CancellationToken cancellationToken)
    {
        var updated = await _grafikPracyService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _grafikPracyService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
