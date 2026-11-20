using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTOs.Auth;

public class RefreshTokenRequestDto
{
    [Required]
    [MinLength(1)]
    public string RefreshToken { get; set; } = null!;
}
