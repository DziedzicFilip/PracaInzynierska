using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/obecnosci")]
[Authorize]
public class ObecnoscController : ControllerBase
{
    private readonly IObecnoscService _obecnoscService;

    public ObecnoscController(IObecnoscService obecnoscService)
    {
        _obecnoscService = obecnoscService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<ObecnoscDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _obecnoscService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ObecnoscDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _obecnoscService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<ObecnoscDto>> Create(ObecnoscDto dto, CancellationToken cancellationToken)
    {
        var created = await _obecnoscService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, ObecnoscDto dto, CancellationToken cancellationToken)
    {
        var updated = await _obecnoscService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _obecnoscService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
