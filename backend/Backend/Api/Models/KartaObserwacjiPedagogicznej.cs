namespace Api.Models;

public class KartaObserwacjiPedagogicznej
{
    public int Id { get; set; }
    public int DzieckoId { get; set; }
    public Dziecko Dziecko { get; set; } = null!;
    public int PracownikId { get; set; }
    public Pracownik Pracownik { get; set; } = null!;
    public string Specjalizacja { get; set; } = null!;
    public string Notatka { get; set; } = null!;
    public DateTime DataWpisu { get; set; }
}

