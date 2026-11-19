namespace Api.Models;

public class Grupa
{
    public int Id { get; set; }
    public string Nazwa { get; set; } = null!;
    public string WiekDzieci { get; set; } = null!;
    public int WychowawcaId { get; set; }
    public Pracownik Wychowawca { get; set; } = null!;
    public int SalaId { get; set; }
    public Sala Sala { get; set; } = null!;
    public ICollection<Dziecko> Dzieci { get; set; } = new List<Dziecko>();
}

