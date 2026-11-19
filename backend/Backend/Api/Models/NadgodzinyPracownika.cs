namespace Api.Models;

public class NadgodzinyPracownika
{
    public int Id { get; set; }
    public int PracownikId { get; set; }
    public Pracownik Pracownik { get; set; } = null!;
    public DateOnly Data { get; set; }
    public decimal IloscGodzin { get; set; }
    public string? Powod { get; set; }
}

