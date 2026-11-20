using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/pomoce-dydaktyczne")]
[Authorize]
public class PomocDydaktycznaController : ControllerBase
{
    private readonly IPomocDydaktycznaService _pomocDydaktycznaService;

    public PomocDydaktycznaController(IPomocDydaktycznaService pomocDydaktycznaService)
    {
        _pomocDydaktycznaService = pomocDydaktycznaService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<PomocDydaktycznaDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _pomocDydaktycznaService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PomocDydaktycznaDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _pomocDydaktycznaService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<PomocDydaktycznaDto>> Create(PomocDydaktycznaDto dto, CancellationToken cancellationToken)
    {
        var created = await _pomocDydaktycznaService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, PomocDydaktycznaDto dto, CancellationToken cancellationToken)
    {
        var updated = await _pomocDydaktycznaService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _pomocDydaktycznaService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
