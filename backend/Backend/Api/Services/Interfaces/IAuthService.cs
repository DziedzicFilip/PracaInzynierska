using Api.Models.DTOs.Auth;

namespace Api.Services.Interfaces;

public interface IAuthService
{
    Task<(bool Succeeded, AuthResponseDto? Response)> LoginAsync(
        LoginRequestDto request,
        CancellationToken cancellationToken = default);

    Task<(bool Succeeded, AuthResponseDto? Response)> RefreshTokenAsync(
        RefreshTokenRequestDto request,
        CancellationToken cancellationToken = default);

    Task<bool> RevokeTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default);
}
