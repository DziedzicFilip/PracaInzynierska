using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.UnitTests;

public class TokenServiceTests
{
    [Test]
    public async Task GenerateAccessTokenAsync_contains_required_claims_and_role()
    {
        using var provider = TestServiceProvider.Create();
        var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = provider.GetRequiredService<RoleManager<ApplicationRole>>();
        await roleManager.CreateAsync(new ApplicationRole { Name = SystemRoles.Administrator });

        var user = new ApplicationUser
        {
            UserName = "admin",
            Email = "admin@example.local"
        };
        await userManager.CreateAsync(user);
        await userManager.AddToRoleAsync(user, SystemRoles.Administrator);

        var tokenService = new TokenService(
            Options.Create(TestServiceProvider.JwtSettings),
            userManager);

        var (encodedToken, expiresAtUtc) =
            await tokenService.GenerateAccessTokenAsync(user);

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(encodedToken);
        Assert.That(jwt.Issuer, Is.EqualTo(TestServiceProvider.JwtSettings.Issuer));
        Assert.That(jwt.Audiences, Does.Contain(TestServiceProvider.JwtSettings.Audience));
        Assert.That(jwt.Claims.Any(claim => claim.Type == ClaimTypes.NameIdentifier && claim.Value == user.Id.ToString()), Is.True);
        Assert.That(jwt.Claims.Any(claim => claim.Type == ClaimTypes.Name && claim.Value == "admin"), Is.True);
        Assert.That(jwt.Claims.Any(claim => claim.Type == ClaimTypes.Email), Is.True);
        Assert.That(jwt.Claims.Any(claim => claim.Type == JwtRegisteredClaimNames.Jti), Is.True);
        Assert.That(jwt.Claims.Any(claim => claim.Type == ClaimTypes.Role && claim.Value == SystemRoles.Administrator), Is.True);
        Assert.That(expiresAtUtc, Is.GreaterThan(DateTime.UtcNow));
    }

    [Test]
    public void GenerateRefreshToken_returns_unique_cryptographic_tokens()
    {
        using var provider = TestServiceProvider.Create();
        var tokenService = new TokenService(
            Options.Create(TestServiceProvider.JwtSettings),
            provider.GetRequiredService<UserManager<ApplicationUser>>());

        var first = tokenService.GenerateRefreshToken();
        var second = tokenService.GenerateRefreshToken();

        Assert.That(first, Is.Not.Empty);
        Assert.That(second, Is.Not.Empty);
        Assert.That(first, Is.Not.EqualTo(second));
    }
}
