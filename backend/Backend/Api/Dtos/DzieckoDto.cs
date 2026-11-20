using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class DzieckoDto
{
    public int Id { get; set; }
    [Required]
    public string Imie { get; set; } = null!;
    [Required]
    public string Nazwisko { get; set; } = null!;
    public DateOnly DataUrodzenia { get; set; }
    public int? GrupaId { get; set; }
}
