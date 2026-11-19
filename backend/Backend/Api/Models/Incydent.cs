namespace Api.Models;

public class Incydent
{
    public int Id { get; set; }
    public int DzieckoId { get; set; }
    public Dziecko Dziecko { get; set; } = null!;
    public int? RodzicId { get; set; }
    public Rodzic? Rodzic { get; set; }
    public int PracownikId { get; set; }
    public Pracownik Pracownik { get; set; } = null!;
    public string Powod { get; set; } = null!;
    public string TypIncydentu { get; set; } = "zdrowotny";
    public DateTime DataZgloszenia { get; set; }
    public bool ProtokolBhP { get; set; }
}

