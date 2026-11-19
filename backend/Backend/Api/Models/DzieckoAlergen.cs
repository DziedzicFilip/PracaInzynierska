namespace Api.Models;

public class DzieckoAlergen
{
    public int DzieckoId { get; set; }
    public Dziecko Dziecko { get; set; } = null!;
    public int AlergenId { get; set; }
    public Alergen Alergen { get; set; } = null!;
}

