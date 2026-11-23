using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/ogloszenia")]
[Authorize]
public class OgloszenieController : ControllerBase
{
    private readonly IOgloszenieService _ogloszenieService;

    public OgloszenieController(IOgloszenieService ogloszenieService)
    {
        _ogloszenieService = ogloszenieService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<OgloszenieDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _ogloszenieService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OgloszenieDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _ogloszenieService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<OgloszenieDto>> Create(OgloszenieDto dto, CancellationToken cancellationToken)
    {
        var created = await _ogloszenieService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, OgloszenieDto dto, CancellationToken cancellationToken)
    {
        var updated = await _ogloszenieService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _ogloszenieService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
