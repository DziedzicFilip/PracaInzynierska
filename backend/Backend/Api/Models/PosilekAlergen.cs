namespace Api.Models;

public class PosilekAlergen
{
    public int PosilekId { get; set; }
    public Posilek Posilek { get; set; } = null!;
    public int AlergenId { get; set; }
    public Alergen Alergen { get; set; } = null!;
}

