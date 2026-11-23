using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/diety")]
[Authorize]
public class DietaController : ControllerBase
{
    private readonly IDietaService _dietaService;

    public DietaController(IDietaService dietaService)
    {
        _dietaService = dietaService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<DietaDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _dietaService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DietaDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _dietaService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<DietaDto>> Create(DietaDto dto, CancellationToken cancellationToken)
    {
        var created = await _dietaService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, DietaDto dto, CancellationToken cancellationToken)
    {
        var updated = await _dietaService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _dietaService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
