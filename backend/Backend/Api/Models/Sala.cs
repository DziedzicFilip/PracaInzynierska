namespace Api.Models;

public class Sala
{
    public int Id { get; set; }
    public string Nazwa { get; set; } = null!;
    public string TypSali { get; set; } = "dydaktyczna";
    public ICollection<PomocDydaktyczna> Pomoce { get; set; } = new List<PomocDydaktyczna>();
    public ICollection<Grupa> Grupy { get; set; } = new List<Grupa>();
    public ICollection<RezerwacjaSali> Rezerwacje { get; set; } = new List<RezerwacjaSali>();
    public ICollection<PlanZajec> PlanyZajec { get; set; } = new List<PlanZajec>();
}

