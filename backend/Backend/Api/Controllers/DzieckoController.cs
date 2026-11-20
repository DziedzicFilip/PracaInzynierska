using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/dzieci")]
[Authorize]
public class DzieckoController : ControllerBase
{
    private readonly IDzieckoService _dzieckoService;

    public DzieckoController(IDzieckoService dzieckoService)
    {
        _dzieckoService = dzieckoService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<DzieckoDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _dzieckoService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DzieckoDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _dzieckoService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<DzieckoDto>> Create(DzieckoDto dto, CancellationToken cancellationToken)
    {
        var created = await _dzieckoService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, DzieckoDto dto, CancellationToken cancellationToken)
    {
        var updated = await _dzieckoService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _dzieckoService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
