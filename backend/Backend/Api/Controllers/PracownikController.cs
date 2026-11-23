using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/pracownicy")]
[Authorize]
public class PracownikController : ControllerBase
{
    private readonly IPracownikService _pracownikService;

    public PracownikController(IPracownikService pracownikService)
    {
        _pracownikService = pracownikService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<PracownikDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _pracownikService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PracownikDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _pracownikService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<PracownikDto>> Create(PracownikDto dto, CancellationToken cancellationToken)
    {
        var created = await _pracownikService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, PracownikDto dto, CancellationToken cancellationToken)
    {
        var updated = await _pracownikService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _pracownikService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
