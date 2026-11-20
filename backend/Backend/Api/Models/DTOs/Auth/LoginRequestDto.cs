using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTOs.Auth;

public class LoginRequestDto
{
    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(128)]
    public string Password { get; set; } = null!;
}
