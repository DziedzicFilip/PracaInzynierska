using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class RodzicDto
{
    public int Id { get; set; }
    public int UzytkownikId { get; set; }
    [Required]
    public string Imie { get; set; } = null!;
    [Required]
    public string Nazwisko { get; set; } = null!;
    public string? KodPinOdbior { get; set; }
}
