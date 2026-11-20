using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/rodzice")]
[Authorize]
public class RodzicController : ControllerBase
{
    private readonly IRodzicService _rodzicService;

    public RodzicController(IRodzicService rodzicService)
    {
        _rodzicService = rodzicService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<RodzicDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _rodzicService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RodzicDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _rodzicService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<RodzicDto>> Create(RodzicDto dto, CancellationToken cancellationToken)
    {
        var created = await _rodzicService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, RodzicDto dto, CancellationToken cancellationToken)
    {
        var updated = await _rodzicService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _rodzicService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
