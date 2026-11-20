using Api.Dtos;
using Api.Services.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/sale")]
[Authorize]
public class SaleController : ControllerBase
{
    private readonly ISalaService _salaService;

    public SaleController(ISalaService salaService)
    {
        _salaService = salaService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<SalaDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var sale = await _salaService.GetAllAsync(cancellationToken);
        return Ok(sale);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SalaDto>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var sala = await _salaService.GetByIdAsync(id, cancellationToken);
        return sala is null ? NotFound() : Ok(sala);
    }

    [HttpPost]
    [Authorize(Roles = SystemRoles.Administrator)]
    public async Task<ActionResult<SalaDto>> Create(
        SalaDto sala,
        CancellationToken cancellationToken)
    {
        var createdSala = await _salaService.CreateAsync(sala, cancellationToken);
        return CreatedAtAction(
            nameof(GetById),
            new { id = createdSala.Id },
            createdSala);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = SystemRoles.Administrator)]
    public async Task<IActionResult> Update(
        int id,
        SalaDto sala,
        CancellationToken cancellationToken)
    {
        var updated = await _salaService.UpdateAsync(id, sala, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = SystemRoles.Administrator)]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var deleted = await _salaService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
