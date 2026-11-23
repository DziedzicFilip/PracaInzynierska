using Api.Dtos;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/nieobecnosci-pracownikow")]
[Authorize]
public class NieobecnoscPracownikaController : ControllerBase
{
    private readonly INieobecnoscPracownikaService _nieobecnoscPracownikaService;

    public NieobecnoscPracownikaController(INieobecnoscPracownikaService nieobecnoscPracownikaService)
    {
        _nieobecnoscPracownikaService = nieobecnoscPracownikaService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<NieobecnoscPracownikaDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _nieobecnoscPracownikaService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<NieobecnoscPracownikaDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _nieobecnoscPracownikaService.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<ActionResult<NieobecnoscPracownikaDto>> Create(NieobecnoscPracownikaDto dto, CancellationToken cancellationToken)
    {
        var created = await _nieobecnoscPracownikaService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Update(int id, NieobecnoscPracownikaDto dto, CancellationToken cancellationToken)
    {
        var updated = await _nieobecnoscPracownikaService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdministrator}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _nieobecnoscPracownikaService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
