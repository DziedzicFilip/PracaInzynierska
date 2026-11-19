namespace Api.Models;

public class WydarzenieKalendarz
{
    public int Id { get; set; }
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
    public DateOnly DataWydarzenia { get; set; }
    public TimeOnly? GodzinaRozpoczecia { get; set; }
    public TimeOnly? GodzinaZakonczenia { get; set; }
    public int? PracownikOdpowiedzialnyId { get; set; }
    public Pracownik? PracownikOdpowiedzialny { get; set; }
}

