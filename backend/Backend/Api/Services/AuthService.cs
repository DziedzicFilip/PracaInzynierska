using Api.Data;
using Api.Models;
using Api.Models.DTOs.Auth;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Api.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _settings;

    public AuthService(
        ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ITokenService tokenService,
        IOptions<JwtSettings> settings)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _settings = settings.Value;
    }

    public async Task<(bool Succeeded, AuthResponseDto? Response)> LoginAsync(
        LoginRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return (false, null);
        }

        var response = await CreateSessionAsync(user, cancellationToken);
        return (true, response);
    }

    public async Task<(bool Succeeded, AuthResponseDto? Response)> RefreshTokenAsync(
        RefreshTokenRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var storedToken = await _dbContext.RefreshTokens
            .Include(token => token.User)
            .FirstOrDefaultAsync(token => token.Token == request.RefreshToken, cancellationToken);

        if (storedToken is null)
        {
            return (false, null);
        }

        if (storedToken.IsRevoked)
        {
            await RevokeAllActiveTokensAsync(storedToken.UserId, cancellationToken);
            return (false, null);
        }

        if (storedToken.ExpiresAtUtc <= DateTime.UtcNow)
        {
            return (false, null);
        }

        storedToken.IsRevoked = true;
        var response = await CreateSessionAsync(storedToken.User, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return (true, response);
    }

    public async Task<bool> RevokeTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var storedToken = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(token => token.Token == refreshToken, cancellationToken);

        if (storedToken is null)
        {
            return false;
        }

        if (!storedToken.IsRevoked)
        {
            storedToken.IsRevoked = true;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return true;
    }

    private async Task<AuthResponseDto> CreateSessionAsync(
        ApplicationUser user,
        CancellationToken cancellationToken)
    {
        var (accessToken, accessTokenExpiresAtUtc) =
            await _tokenService.GenerateAccessTokenAsync(user);
        var refreshTokenValue = _tokenService.GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            Token = refreshTokenValue,
            ExpiresAtUtc = DateTime.UtcNow.AddDays(_settings.RefreshTokenExpirationDays),
            CreatedAtUtc = DateTime.UtcNow,
            IsRevoked = false,
            UserId = user.Id
        };

        _dbContext.RefreshTokens.Add(refreshToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var roles = await _userManager.GetRolesAsync(user);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshTokenValue,
            AccessTokenExpiresAtUtc = accessTokenExpiresAtUtc,
            UserId = user.Id,
            UserName = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            Roles = roles.ToArray()
        };
    }

    private async Task RevokeAllActiveTokensAsync(
        int userId,
        CancellationToken cancellationToken)
    {
        var activeTokens = await _dbContext.RefreshTokens
            .Where(token => token.UserId == userId && !token.IsRevoked)
            .ToListAsync(cancellationToken);

        foreach (var token in activeTokens)
        {
            token.IsRevoked = true;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
