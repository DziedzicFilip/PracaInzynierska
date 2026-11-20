namespace Api.Models;

public static class SystemRoles
{
    public const string Administrator = "Administrator";
    public const string Wychowawca = "Wychowawca";
    public const string Rodzic = "Rodzic";

    public static IReadOnlyCollection<string> All { get; } =
        new[] { Administrator, Wychowawca, Rodzic };
}
