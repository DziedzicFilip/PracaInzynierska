using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/grupy")]
[Authorize]
public class GrupaController : ControllerBase
{
    private readonly IGrupaService _grupaService;

    public GrupaController(IGrupaService grupaService)
    {
        _grupaService = grupaService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<GrupaDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _grupaService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GrupaDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _grupaService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<GrupaDto>> Create(GrupaDto dto, CancellationToken cancellationToken)
    {
        var created = await _grupaService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, GrupaDto dto, CancellationToken cancellationToken)
    {
        var updated = await _grupaService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _grupaService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
