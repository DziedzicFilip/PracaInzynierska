using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/przeciwwskazania-medyczne")]
[Authorize]
public class PrzeciwskazanieMedyczneController : ControllerBase
{
    private readonly IPrzeciwskazanieMedyczneService _przeciwskazanieMedyczneService;

    public PrzeciwskazanieMedyczneController(IPrzeciwskazanieMedyczneService przeciwskazanieMedyczneService)
    {
        _przeciwskazanieMedyczneService = przeciwskazanieMedyczneService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<PrzeciwskazanieMedyczneDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _przeciwskazanieMedyczneService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PrzeciwskazanieMedyczneDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _przeciwskazanieMedyczneService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<PrzeciwskazanieMedyczneDto>> Create(PrzeciwskazanieMedyczneDto dto, CancellationToken cancellationToken)
    {
        var created = await _przeciwskazanieMedyczneService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, PrzeciwskazanieMedyczneDto dto, CancellationToken cancellationToken)
    {
        var updated = await _przeciwskazanieMedyczneService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _przeciwskazanieMedyczneService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
