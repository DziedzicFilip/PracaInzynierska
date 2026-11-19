namespace Api.Models;

public class PomocDydaktyczna
{
    public int Id { get; set; }
    public string Nazwa { get; set; } = null!;
    public string Typ { get; set; } = null!;
    public int StanIlosciowy { get; set; } = 1;
    public int? SalaPrzypisanaId { get; set; }
    public Sala? SalaPrzypisana { get; set; }
    public ICollection<RezerwacjaPomocy> Rezerwacje { get; set; } = new List<RezerwacjaPomocy>();
    public ICollection<UsterkaPomocy> Usterki { get; set; } = new List<UsterkaPomocy>();
}

