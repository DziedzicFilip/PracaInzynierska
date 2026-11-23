using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class DziennikZajecDto
{
    public int Id { get; set; }
    public int GrupaId { get; set; }
    public int PracownikId { get; set; }
    public DateOnly Data { get; set; }
    [Required]
    public string Temat { get; set; } = null!;
    public string? PodstawaProgramowa { get; set; }
}
