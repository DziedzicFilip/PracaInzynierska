namespace Api.Models;

public class RejestrPodaniaLekow
{
    public int Id { get; set; }
    public int LekId { get; set; }
    public LekDziecka Lek { get; set; } = null!;
    public int PracownikId { get; set; }
    public Pracownik Pracownik { get; set; } = null!;
    public DateTime DataPodania { get; set; }
    public bool PotwierdzeniePodpisu { get; set; } = true;
}

