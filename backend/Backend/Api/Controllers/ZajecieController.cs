using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/zajecia")]
[Authorize]
public class ZajecieController : ControllerBase
{
    private readonly IZajecieService _zajecieService;

    public ZajecieController(IZajecieService zajecieService)
    {
        _zajecieService = zajecieService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<ZajecieDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _zajecieService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ZajecieDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _zajecieService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<ZajecieDto>> Create(ZajecieDto dto, CancellationToken cancellationToken)
    {
        var created = await _zajecieService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, ZajecieDto dto, CancellationToken cancellationToken)
    {
        var updated = await _zajecieService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _zajecieService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
