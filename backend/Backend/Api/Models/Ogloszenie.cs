namespace Api.Models;

public class Ogloszenie
{
    public int Id { get; set; }
    public int AutorId { get; set; }
    public Pracownik Autor { get; set; } = null!;
    public int? GrupaId { get; set; }
    public Grupa? Grupa { get; set; }
    public string Tytul { get; set; } = null!;
    public string Tresc { get; set; } = null!;
    public DateTime DataPublikacji { get; set; }
}

