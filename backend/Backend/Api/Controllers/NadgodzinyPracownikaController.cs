using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/nadgodziny-pracownikow")]
[Authorize]
public class NadgodzinyPracownikaController : ControllerBase
{
    private readonly INadgodzinyPracownikaService _nadgodzinyPracownikaService;

    public NadgodzinyPracownikaController(INadgodzinyPracownikaService nadgodzinyPracownikaService)
    {
        _nadgodzinyPracownikaService = nadgodzinyPracownikaService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<NadgodzinyPracownikaDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _nadgodzinyPracownikaService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<NadgodzinyPracownikaDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _nadgodzinyPracownikaService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<NadgodzinyPracownikaDto>> Create(NadgodzinyPracownikaDto dto, CancellationToken cancellationToken)
    {
        var created = await _nadgodzinyPracownikaService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, NadgodzinyPracownikaDto dto, CancellationToken cancellationToken)
    {
        var updated = await _nadgodzinyPracownikaService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _nadgodzinyPracownikaService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
