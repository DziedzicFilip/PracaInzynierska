using Api.Data;
using Api.Models;
using Api.Models.DTOs.Auth;
using Api.Services;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Api.UnitTests;

public class AuthServiceTests
{
    [Test]
    public async Task Login_and_revoke_refresh_token_work()
    {
        using var provider = TestServiceProvider.Create();
        var auth = await CreateAuthServiceAsync(provider);
        await CreateUserAsync(provider, "parent@example.local", "parent");

        var login = await auth.LoginAsync(new LoginRequestDto
        {
            Email = "parent@example.local",
            Password = "Password1!"
        });
        Assert.That(login.Succeeded, Is.True);
        Assert.That(login.Response!.Roles, Does.Contain(SystemRoles.Rodzic));

        var revoked = await auth.RevokeTokenAsync(login.Response!.RefreshToken);
        Assert.That(revoked, Is.True);

        var refreshAfterRevoke = await auth.RefreshTokenAsync(new RefreshTokenRequestDto
        {
            RefreshToken = login.Response.RefreshToken
        });
        Assert.That(refreshAfterRevoke.Succeeded, Is.False);
    }

    [Test]
    public async Task Refresh_rotates_token_and_reuse_revokes_all_active_tokens()
    {
        using var provider = TestServiceProvider.Create();
        var auth = await CreateAuthServiceAsync(provider);
        await CreateUserAsync(provider, "parent2@example.local", "parent2");

        var login = await auth.LoginAsync(new LoginRequestDto
        {
            Email = "parent2@example.local",
            Password = "Password1!"
        });

        Assert.That(login.Succeeded, Is.True);
        var oldRefreshToken = login.Response!.RefreshToken;
        var refresh = await auth.RefreshTokenAsync(new RefreshTokenRequestDto
        {
            RefreshToken = oldRefreshToken
        });

        Assert.That(refresh.Succeeded, Is.True);
        Assert.That(refresh.Response!.RefreshToken, Is.Not.EqualTo(oldRefreshToken));

        var db = provider.GetRequiredService<ApplicationDbContext>();
        var oldToken = await db.RefreshTokens.SingleAsync(token => token.Token == oldRefreshToken);
        Assert.That(oldToken.IsRevoked, Is.True);

        var reused = await auth.RefreshTokenAsync(new RefreshTokenRequestDto
        {
            RefreshToken = oldRefreshToken
        });
        Assert.That(reused.Succeeded, Is.False);

        var activeTokens = await db.RefreshTokens
            .Where(token => !token.IsRevoked)
            .CountAsync();
        Assert.That(activeTokens, Is.Zero);
    }

    private static async Task CreateUserAsync(
        IServiceProvider provider,
        string email,
        string userName)
    {
        var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
        var user = new ApplicationUser
        {
            Email = email,
            UserName = userName
        };

        var createResult = await userManager.CreateAsync(user, "Password1!");
        Assert.That(createResult.Succeeded, Is.True,
            string.Join("; ", createResult.Errors.Select(error => error.Description)));

        var roleResult = await userManager.AddToRoleAsync(user, SystemRoles.Rodzic);
        Assert.That(roleResult.Succeeded, Is.True);
    }

    private static async Task<IAuthService> CreateAuthServiceAsync(IServiceProvider provider)
    {
        var roleManager = provider.GetRequiredService<RoleManager<ApplicationRole>>();
        await roleManager.CreateAsync(new ApplicationRole { Name = SystemRoles.Rodzic });

        var tokenService = new TokenService(
            Options.Create(TestServiceProvider.JwtSettings),
            provider.GetRequiredService<UserManager<ApplicationUser>>());

        return new AuthService(
            provider.GetRequiredService<ApplicationDbContext>(),
            provider.GetRequiredService<UserManager<ApplicationUser>>(),
            roleManager,
            tokenService,
            Options.Create(TestServiceProvider.JwtSettings));
    }
}
