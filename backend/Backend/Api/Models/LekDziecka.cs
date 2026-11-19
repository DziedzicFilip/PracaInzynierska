namespace Api.Models;

public class LekDziecka
{
    public int Id { get; set; }
    public int DzieckoId { get; set; }
    public Dziecko Dziecko { get; set; } = null!;
    public string NazwaLeku { get; set; } = null!;
    public string Dawka { get; set; } = null!;
    public TimeOnly GodzinaPodania { get; set; }
    public DateOnly? DataOd { get; set; }
    public DateOnly? DataDo { get; set; }
    public string? Uwagi { get; set; }
    public bool ZgodaRodzica { get; set; } = true;
}

