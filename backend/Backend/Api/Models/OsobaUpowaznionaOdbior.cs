namespace Api.Models;

public class OsobaUpowaznionaOdbior
{
    public int Id { get; set; }
    public int DzieckoId { get; set; }
    public Dziecko Dziecko { get; set; } = null!;
    public string Imie { get; set; } = null!;
    public string Nazwisko { get; set; } = null!;
    public string? JednorazowyKodPin { get; set; }
}

