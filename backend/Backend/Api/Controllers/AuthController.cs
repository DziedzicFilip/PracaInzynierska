using Api.Models.DTOs.Auth;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Loguje użytkownika i zwraca parę access/refresh token.
    /// </summary>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(
        LoginRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request, cancellationToken);
        return result.Succeeded
            ? Ok(result.Response)
            : Unauthorized(new { Error = "Nieprawidłowy e-mail lub hasło." });
    }

    /// <summary>
    /// Rotuje refresh token i zwraca nową parę tokenów.
    /// </summary>
    [AllowAnonymous]
    [HttpPost("refresh")]
   
    public async Task<ActionResult<AuthResponseDto>> Refresh(
        RefreshTokenRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _authService.RefreshTokenAsync(request, cancellationToken);
        return result.Succeeded
            ? Ok(result.Response)
            : Unauthorized(new { Error = "Refresh token jest nieprawidłowy, wygasły lub unieważniony." });
    }

    /// <summary>
    /// Unieważnia wskazany refresh token.
    /// </summary>
    [Authorize]
    [HttpPost("revoke")]

    public async Task<IActionResult> Revoke(
        RefreshTokenRequestDto request,
        CancellationToken cancellationToken)
    {
        var revoked = await _authService.RevokeTokenAsync(
            request.RefreshToken,
            cancellationToken);

        return revoked ? Ok(new { Message = "Refresh token został unieważniony." }) : NotFound();
    }
}
