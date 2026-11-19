namespace Api.Models;

public class Zajecie
{
    public int Id { get; set; }
    public string Nazwa { get; set; } = null!;
    public string TypZajec { get; set; } = "podstawowe";
    public ICollection<PlanZajec> PlanyZajec { get; set; } = new List<PlanZajec>();
}

