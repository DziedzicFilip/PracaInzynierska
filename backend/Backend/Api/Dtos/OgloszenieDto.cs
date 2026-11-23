using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class OgloszenieDto
{
    public int Id { get; set; }
    public int AutorId { get; set; }
    public int? GrupaId { get; set; }
    [Required]
    public string Tytul { get; set; } = null!;
    [Required]
    public string Tresc { get; set; } = null!;
    public DateTime DataPublikacji { get; set; }
}
