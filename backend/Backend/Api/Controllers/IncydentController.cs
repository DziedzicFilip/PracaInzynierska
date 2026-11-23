using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/incydenty")]
[Authorize]
public class IncydentController : ControllerBase
{
    private readonly IIncydentService _incydentService;

    public IncydentController(IIncydentService incydentService)
    {
        _incydentService = incydentService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<IncydentDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _incydentService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<IncydentDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _incydentService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<IncydentDto>> Create(IncydentDto dto, CancellationToken cancellationToken)
    {
        var created = await _incydentService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, IncydentDto dto, CancellationToken cancellationToken)
    {
        var updated = await _incydentService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _incydentService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
