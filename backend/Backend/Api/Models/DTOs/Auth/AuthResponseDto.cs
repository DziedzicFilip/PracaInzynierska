namespace Api.Models.DTOs.Auth;

public class AuthResponseDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime AccessTokenExpiresAtUtc { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public IReadOnlyCollection<string> Roles { get; set; } = Array.Empty<string>();
}
