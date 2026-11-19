namespace Api.Models;

public class Dziecko
{
    public int Id { get; set; }
    public string Imie { get; set; } = null!;
    public string Nazwisko { get; set; } = null!;
    public DateOnly DataUrodzenia { get; set; }
    public int? GrupaId { get; set; }
    public Grupa? Grupa { get; set; }
    public ICollection<RodzicDziecko> Rodzice { get; set; } = new List<RodzicDziecko>();
    public ICollection<DzieckoAlergen> Alergeny { get; set; } = new List<DzieckoAlergen>();
    public ICollection<DzieckoDieta> Diety { get; set; } = new List<DzieckoDieta>();
}

