using Api.Models;

namespace Api.Services.Interfaces;

public interface ITokenService
{
    Task<(string AccessToken, DateTime ExpiresAtUtc)> GenerateAccessTokenAsync(ApplicationUser user);
    string GenerateRefreshToken();
}
