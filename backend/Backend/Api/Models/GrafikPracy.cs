namespace Api.Models;

public class GrafikPracy
{
    public int Id { get; set; }
    public int PracownikId { get; set; }
    public Pracownik Pracownik { get; set; } = null!;
    public DateOnly Data { get; set; }
    public TimeOnly GodzinaRozpoczecia { get; set; }
    public TimeOnly GodzinaZakonczenia { get; set; }
}

