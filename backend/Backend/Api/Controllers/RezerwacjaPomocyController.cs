using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/rezerwacje-pomocy")]
[Authorize]
public class RezerwacjaPomocyController : ControllerBase
{
    private readonly IRezerwacjaPomocyService _rezerwacjaPomocyService;

    public RezerwacjaPomocyController(IRezerwacjaPomocyService rezerwacjaPomocyService)
    {
        _rezerwacjaPomocyService = rezerwacjaPomocyService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<RezerwacjaPomocyDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _rezerwacjaPomocyService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RezerwacjaPomocyDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _rezerwacjaPomocyService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<RezerwacjaPomocyDto>> Create(RezerwacjaPomocyDto dto, CancellationToken cancellationToken)
    {
        var created = await _rezerwacjaPomocyService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, RezerwacjaPomocyDto dto, CancellationToken cancellationToken)
    {
        var updated = await _rezerwacjaPomocyService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _rezerwacjaPomocyService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
