using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/alergeny")]
[Authorize]
public class AlergenyController : ControllerBase
{
    private readonly IAlergenService _alergenService;

    public AlergenyController(IAlergenService alergenService)
    {
        _alergenService = alergenService;
    }

    
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<AlergenDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<AlergenDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var alergeny = await _alergenService.GetAllAsync(cancellationToken);
        return Ok(alergeny);
    }


    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AlergenDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AlergenDto>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var alergen = await _alergenService.GetByIdAsync(id, cancellationToken);
        return alergen is null ? NotFound() : Ok(alergen);
    }

    
    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]

    public async Task<ActionResult<AlergenDto>> Create(
        AlergenDto alergen,
        CancellationToken cancellationToken)
    {
        var createdAlergen = await _alergenService.CreateAsync(
            alergen,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdAlergen.Id },
            createdAlergen);
    }

  
    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    
    public async Task<IActionResult> Update(
        int id,
        AlergenDto alergen,
        CancellationToken cancellationToken)
    {
        var updated = await _alergenService.UpdateAsync(
            id,
            alergen,
            cancellationToken);

        return updated ? NoContent() : NotFound();
    }

    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]

    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var deleted = await _alergenService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
