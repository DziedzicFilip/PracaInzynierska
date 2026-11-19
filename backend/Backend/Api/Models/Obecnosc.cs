namespace Api.Models;

public class Obecnosc
{
    public int Id { get; set; }
    public int DzieckoId { get; set; }
    public Dziecko Dziecko { get; set; } = null!;
    public DateOnly Data { get; set; }
    public string Status { get; set; } = null!;
    public TimeOnly? GodzinaPrzyjscia { get; set; }
    public TimeOnly? GodzinaOdbioru { get; set; }
    public int? RodzicOdbierajacyId { get; set; }
    public Rodzic? RodzicOdbierajacy { get; set; }
    public int? OsobaUpowaznionaId { get; set; }
    public OsobaUpowaznionaOdbior? OsobaUpowazniona { get; set; }
    public string? MetodaRejestracji { get; set; }
}

