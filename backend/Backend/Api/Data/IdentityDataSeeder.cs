using Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Api.Data;

public static class IdentityDataSeeder
{
    public static async Task SeedAsync(IServiceProvider services, IConfiguration configuration)
    {
        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (var roleName in SystemRoles.All)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
            }
        }

        var superAdminUserName =
            configuration["SUPERADMIN_USERNAME"] ?? Environment.GetEnvironmentVariable("SUPERADMIN_USERNAME");
        var superAdminEmail =
            configuration["SUPERADMIN_EMAIL"] ?? Environment.GetEnvironmentVariable("SUPERADMIN_EMAIL");
        var superAdminPassword =
            configuration["SUPERADMIN_PASSWORD"] ?? Environment.GetEnvironmentVariable("SUPERADMIN_PASSWORD");

        if (string.IsNullOrWhiteSpace(superAdminUserName) ||
            string.IsNullOrWhiteSpace(superAdminEmail) ||
            string.IsNullOrWhiteSpace(superAdminPassword))
        {
            throw new InvalidOperationException(
                "W .env muszą być ustawione: SUPERADMIN_USERNAME, SUPERADMIN_EMAIL i SUPERADMIN_PASSWORD.");
        }

        var superAdmin = await userManager.FindByEmailAsync(superAdminEmail);
        if (superAdmin is null)
        {
            superAdmin = new ApplicationUser
            {
                UserName = superAdminUserName,
                Email = superAdminEmail,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(superAdmin, superAdminPassword);
            if (!createResult.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Nie udało się utworzyć konta superadmina: {string.Join("; ", createResult.Errors.Select(error => error.Description))}");
            }
        }

        if (!await userManager.IsInRoleAsync(superAdmin, SystemRoles.SuperAdministrator))
        {
            await userManager.AddToRoleAsync(superAdmin, SystemRoles.SuperAdministrator);
        }

        if (!await userManager.IsInRoleAsync(superAdmin, SystemRoles.Administrator))
        {
            await userManager.AddToRoleAsync(superAdmin, SystemRoles.Administrator);
        }
    }
}
