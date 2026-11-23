using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class PosilekDto
{
    public int Id { get; set; }
    public DateOnly Data { get; set; }
    [Required]
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
}
