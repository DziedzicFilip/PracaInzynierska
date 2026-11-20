using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/osoby-upowaznione-odbior")]
[Authorize]
public class OsobaUpowaznionaOdbiorController : ControllerBase
{
    private readonly IOsobaUpowaznionaOdbiorService _osobaUpowaznionaOdbiorService;

    public OsobaUpowaznionaOdbiorController(IOsobaUpowaznionaOdbiorService osobaUpowaznionaOdbiorService)
    {
        _osobaUpowaznionaOdbiorService = osobaUpowaznionaOdbiorService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<OsobaUpowaznionaOdbiorDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _osobaUpowaznionaOdbiorService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OsobaUpowaznionaOdbiorDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _osobaUpowaznionaOdbiorService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<OsobaUpowaznionaOdbiorDto>> Create(OsobaUpowaznionaOdbiorDto dto, CancellationToken cancellationToken)
    {
        var created = await _osobaUpowaznionaOdbiorService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, OsobaUpowaznionaOdbiorDto dto, CancellationToken cancellationToken)
    {
        var updated = await _osobaUpowaznionaOdbiorService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _osobaUpowaznionaOdbiorService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
