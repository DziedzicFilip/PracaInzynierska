using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/usterki-pomocy")]
[Authorize]
public class UsterkaPomocyController : ControllerBase
{
    private readonly IUsterkaPomocyService _usterkaPomocyService;

    public UsterkaPomocyController(IUsterkaPomocyService usterkaPomocyService)
    {
        _usterkaPomocyService = usterkaPomocyService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<UsterkaPomocyDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _usterkaPomocyService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsterkaPomocyDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _usterkaPomocyService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<UsterkaPomocyDto>> Create(UsterkaPomocyDto dto, CancellationToken cancellationToken)
    {
        var created = await _usterkaPomocyService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, UsterkaPomocyDto dto, CancellationToken cancellationToken)
    {
        var updated = await _usterkaPomocyService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _usterkaPomocyService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
