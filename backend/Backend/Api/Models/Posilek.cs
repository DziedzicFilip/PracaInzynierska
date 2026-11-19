namespace Api.Models;

public class Posilek
{
    public int Id { get; set; }
    public DateOnly Data { get; set; }
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
    public ICollection<PosilekAlergen> Alergeny { get; set; } = new List<PosilekAlergen>();
}

