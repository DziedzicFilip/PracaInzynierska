using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/wydarzenia-kalendarza")]
[Authorize]
public class WydarzenieKalendarzController : ControllerBase
{
    private readonly IWydarzenieKalendarzService _wydarzenieKalendarzService;

    public WydarzenieKalendarzController(IWydarzenieKalendarzService wydarzenieKalendarzService)
    {
        _wydarzenieKalendarzService = wydarzenieKalendarzService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<WydarzenieKalendarzDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _wydarzenieKalendarzService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<WydarzenieKalendarzDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _wydarzenieKalendarzService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<WydarzenieKalendarzDto>> Create(WydarzenieKalendarzDto dto, CancellationToken cancellationToken)
    {
        var created = await _wydarzenieKalendarzService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, WydarzenieKalendarzDto dto, CancellationToken cancellationToken)
    {
        var updated = await _wydarzenieKalendarzService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _wydarzenieKalendarzService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
