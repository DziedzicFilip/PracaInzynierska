using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/rezerwacje-sal")]
[Authorize]
public class RezerwacjaSaliController : ControllerBase
{
    private readonly IRezerwacjaSaliService _rezerwacjaSaliService;

    public RezerwacjaSaliController(IRezerwacjaSaliService rezerwacjaSaliService)
    {
        _rezerwacjaSaliService = rezerwacjaSaliService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<RezerwacjaSaliDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _rezerwacjaSaliService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RezerwacjaSaliDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _rezerwacjaSaliService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<RezerwacjaSaliDto>> Create(RezerwacjaSaliDto dto, CancellationToken cancellationToken)
    {
        var created = await _rezerwacjaSaliService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, RezerwacjaSaliDto dto, CancellationToken cancellationToken)
    {
        var updated = await _rezerwacjaSaliService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _rezerwacjaSaliService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
