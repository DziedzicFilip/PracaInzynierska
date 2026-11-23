using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/rodzic-dziecko")]
[Authorize]
public class RodzicDzieckoController : ControllerBase
{
    private readonly IRodzicDzieckoService _rodzicDzieckoService;

    public RodzicDzieckoController(IRodzicDzieckoService rodzicDzieckoService)
    {
        _rodzicDzieckoService = rodzicDzieckoService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<RodzicDzieckoDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _rodzicDzieckoService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{rodzicId:int}/{dzieckoId:int}")]
    public async Task<ActionResult<RodzicDzieckoDto>> GetById(int rodzicId, int dzieckoId, CancellationToken cancellationToken)
    {
        var dto = await _rodzicDzieckoService.GetByIdAsync(rodzicId, dzieckoId, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<RodzicDzieckoDto>> Create(RodzicDzieckoDto dto, CancellationToken cancellationToken)
    {
        var created = await _rodzicDzieckoService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { rodzicId = created.RodzicId, dzieckoId = created.DzieckoId }, created);
    }

    [HttpPut("{rodzicId:int}/{dzieckoId:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int rodzicId, int dzieckoId, RodzicDzieckoDto dto, CancellationToken cancellationToken)
    {
        var updated = await _rodzicDzieckoService.UpdateAsync(rodzicId, dzieckoId, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{rodzicId:int}/{dzieckoId:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int rodzicId, int dzieckoId, CancellationToken cancellationToken)
    {
        var deleted = await _rodzicDzieckoService.DeleteAsync(rodzicId, dzieckoId, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
