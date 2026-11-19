namespace Api.Models;

public class Dieta
{
    public int Id { get; set; }
    public string Nazwa { get; set; } = null!;
    public ICollection<DzieckoDieta> Dzieci { get; set; } = new List<DzieckoDieta>();
}

