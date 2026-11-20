using Api.Data;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Api.IntegrationTests;

public class SaleAuthorizationTests
{
    [Test]
    public async Task Request_without_token_returns_401()
    {
        await using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/sale");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Parent_role_without_admin_permission_returns_403()
    {
        await using var factory = CreateFactory();
        var authorization = await EnsureRoleAndUserAsync(
            factory,
            SystemRoles.Rodzic,
            "parent@local.test");
        using var client = factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = authorization;

        var response = await client.PostAsJsonAsync("/api/sale", new
        {
            Nazwa = "Nowa sala",
            TypSali = "dydaktyczna"
        });

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
    }

    [Test]
    public async Task Administrator_role_can_create_sala_and_returns_201()
    {
        await using var factory = CreateFactory();
        var authorization = await EnsureRoleAndUserAsync(
            factory,
            SystemRoles.Administrator,
            "admin@local.test");
        using var client = factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = authorization;

        var response = await client.PostAsJsonAsync("/api/sale", new
        {
            Nazwa = "Sala administracyjna",
            TypSali = "dydaktyczna"
        });

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
    }

    private static WebApplicationFactory<Program> CreateFactory()
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll<ApplicationDbContext>();
                    services.RemoveAll<DbContextOptions<ApplicationDbContext>>();
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseInMemoryDatabase($"authorization-{Guid.NewGuid()}"));
                });
            });
    }

    private static async Task<AuthenticationHeaderValue> EnsureRoleAndUserAsync(
        WebApplicationFactory<Program> factory,
        string role,
        string email)
    {
        using var scope = factory.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new ApplicationRole { Name = role });
        }

        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = email.Split('@')[0],
                Email = email
            };
            await userManager.CreateAsync(user);
        }

        if (!await userManager.IsInRoleAsync(user, role))
        {
            await userManager.AddToRoleAsync(user, role);
        }

        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var (token, _) = await tokenService.GenerateAccessTokenAsync(user);
        return new AuthenticationHeaderValue("Bearer", token);
    }
}
