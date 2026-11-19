namespace Api.Models;

public class RezerwacjaPomocy
{
    public int Id { get; set; }
    public int PomocId { get; set; }
    public PomocDydaktyczna Pomoc { get; set; } = null!;
    public int PracownikId { get; set; }
    public Pracownik Pracownik { get; set; } = null!;
    public DateTime DataOd { get; set; }
    public DateTime DataDo { get; set; }
}

