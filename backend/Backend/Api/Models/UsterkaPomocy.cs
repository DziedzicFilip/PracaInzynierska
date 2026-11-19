namespace Api.Models;

public class UsterkaPomocy
{
    public int Id { get; set; }
    public int PomocId { get; set; }
    public PomocDydaktyczna Pomoc { get; set; } = null!;
    public int PracownikZglaszajacyId { get; set; }
    public Pracownik PracownikZglaszajacy { get; set; } = null!;
    public string Opis { get; set; } = null!;
    public DateTime DataZgloszenia { get; set; }
    public string Status { get; set; } = "ZgĹ‚oszona";
}

