namespace Api.Models;

public class DzieckoDieta
{
    public int DzieckoId { get; set; }
    public Dziecko Dziecko { get; set; } = null!;
    public int DietaId { get; set; }
    public Dieta Dieta { get; set; } = null!;
}

