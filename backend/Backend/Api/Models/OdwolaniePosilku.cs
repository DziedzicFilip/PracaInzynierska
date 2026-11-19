namespace Api.Models;

public class OdwolaniePosilku
{
    public int Id { get; set; }
    public int DzieckoId { get; set; }
    public Dziecko Dziecko { get; set; } = null!;
    public DateOnly DataPosilku { get; set; }
    public DateTime DataZgloszenia { get; set; }
}

