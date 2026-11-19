namespace Api.Models;

public class PlanZajec
{
    public int Id { get; set; }
    public int ZajecieId { get; set; }
    public Zajecie Zajecie { get; set; } = null!;
    public int? GrupaId { get; set; }
    public Grupa? Grupa { get; set; }
    public int SalaId { get; set; }
    public Sala Sala { get; set; } = null!;
    public int PracownikId { get; set; }
    public Pracownik Pracownik { get; set; } = null!;
    public int DzienTygodnia { get; set; }
    public TimeOnly CzasRozpoczecia { get; set; }
    public TimeOnly CzasZakonczenia { get; set; }
    public ICollection<DzieckoPlanZajec> Dzieci { get; set; } = new List<DzieckoPlanZajec>();
}

