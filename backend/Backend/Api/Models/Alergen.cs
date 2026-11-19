namespace Api.Models;

public class Alergen
{
    public int Id { get; set; }
    public string Nazwa { get; set; } = null!;
    public ICollection<PosilekAlergen> Posilki { get; set; } = new List<PosilekAlergen>();
    public ICollection<DzieckoAlergen> Dzieci { get; set; } = new List<DzieckoAlergen>();
}

