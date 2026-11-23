using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/odwolania-posilkow")]
[Authorize]
public class OdwolaniePosilkuController : ControllerBase
{
    private readonly IOdwolaniePosilkuService _odwolaniePosilkuService;

    public OdwolaniePosilkuController(IOdwolaniePosilkuService odwolaniePosilkuService)
    {
        _odwolaniePosilkuService = odwolaniePosilkuService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<OdwolaniePosilkuDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _odwolaniePosilkuService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OdwolaniePosilkuDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _odwolaniePosilkuService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<OdwolaniePosilkuDto>> Create(OdwolaniePosilkuDto dto, CancellationToken cancellationToken)
    {
        var created = await _odwolaniePosilkuService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, OdwolaniePosilkuDto dto, CancellationToken cancellationToken)
    {
        var updated = await _odwolaniePosilkuService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _odwolaniePosilkuService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
