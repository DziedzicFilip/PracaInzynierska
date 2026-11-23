using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/rejestry-podania-lekow")]
[Authorize]
public class RejestrPodaniaLekowController : ControllerBase
{
    private readonly IRejestrPodaniaLekowService _rejestrPodaniaLekowService;

    public RejestrPodaniaLekowController(IRejestrPodaniaLekowService rejestrPodaniaLekowService)
    {
        _rejestrPodaniaLekowService = rejestrPodaniaLekowService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<RejestrPodaniaLekowDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _rejestrPodaniaLekowService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RejestrPodaniaLekowDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _rejestrPodaniaLekowService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<RejestrPodaniaLekowDto>> Create(RejestrPodaniaLekowDto dto, CancellationToken cancellationToken)
    {
        var created = await _rejestrPodaniaLekowService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, RejestrPodaniaLekowDto dto, CancellationToken cancellationToken)
    {
        var updated = await _rejestrPodaniaLekowService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _rejestrPodaniaLekowService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
