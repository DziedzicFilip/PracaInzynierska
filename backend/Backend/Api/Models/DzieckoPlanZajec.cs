namespace Api.Models;

public class DzieckoPlanZajec
{
    public int DzieckoId { get; set; }
    public Dziecko Dziecko { get; set; } = null!;
    public int PlanZajecId { get; set; }
    public PlanZajec PlanZajec { get; set; } = null!;
}

