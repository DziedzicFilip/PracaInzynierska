using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.UnitTests;

internal static class TestServiceProvider
{
    internal static JwtSettings JwtSettings { get; } = new()
    {
        Issuer = "TestIssuer",
        Audience = "TestAudience",
        SecretKey = "Test-secret-key-with-at-least-32-bytes!",
        AccessTokenExpirationMinutes = 30,
        RefreshTokenExpirationDays = 7
    };

    internal static ServiceProvider Create()
    {
        var services = new ServiceCollection();
        var databaseName = Guid.NewGuid().ToString();

        services.AddLogging();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase(databaseName));
        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services.BuildServiceProvider();
    }
}
