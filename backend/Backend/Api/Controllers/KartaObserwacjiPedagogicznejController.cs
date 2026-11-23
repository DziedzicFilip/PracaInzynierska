using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/karty-obserwacji-pedagogicznej")]
[Authorize]
public class KartaObserwacjiPedagogicznejController : ControllerBase
{
    private readonly IKartaObserwacjiPedagogicznejService _kartaObserwacjiPedagogicznejService;

    public KartaObserwacjiPedagogicznejController(IKartaObserwacjiPedagogicznejService kartaObserwacjiPedagogicznejService)
    {
        _kartaObserwacjiPedagogicznejService = kartaObserwacjiPedagogicznejService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<KartaObserwacjiPedagogicznejDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _kartaObserwacjiPedagogicznejService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<KartaObserwacjiPedagogicznejDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _kartaObserwacjiPedagogicznejService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<KartaObserwacjiPedagogicznejDto>> Create(KartaObserwacjiPedagogicznejDto dto, CancellationToken cancellationToken)
    {
        var created = await _kartaObserwacjiPedagogicznejService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, KartaObserwacjiPedagogicznejDto dto, CancellationToken cancellationToken)
    {
        var updated = await _kartaObserwacjiPedagogicznejService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _kartaObserwacjiPedagogicznejService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
