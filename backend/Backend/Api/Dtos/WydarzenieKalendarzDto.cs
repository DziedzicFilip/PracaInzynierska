using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class WydarzenieKalendarzDto
{
    public int Id { get; set; }
    [Required]
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
    public DateOnly DataWydarzenia { get; set; }
    public TimeOnly? GodzinaRozpoczecia { get; set; }
    public TimeOnly? GodzinaZakonczenia { get; set; }
    public int? PracownikOdpowiedzialnyId { get; set; }
}
