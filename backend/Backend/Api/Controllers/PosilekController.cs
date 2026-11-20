using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/posilki")]
[Authorize]
public class PosilekController : ControllerBase
{
    private readonly IPosilekService _posilekService;

    public PosilekController(IPosilekService posilekService)
    {
        _posilekService = posilekService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<PosilekDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _posilekService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PosilekDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _posilekService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<PosilekDto>> Create(PosilekDto dto, CancellationToken cancellationToken)
    {
        var created = await _posilekService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, PosilekDto dto, CancellationToken cancellationToken)
    {
        var updated = await _posilekService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _posilekService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
