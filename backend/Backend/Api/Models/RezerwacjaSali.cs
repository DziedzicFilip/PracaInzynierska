namespace Api.Models;

public class RezerwacjaSali
{
    public int Id { get; set; }
    public int SalaId { get; set; }
    public Sala Sala { get; set; } = null!;
    public int PracownikId { get; set; }
    public Pracownik Pracownik { get; set; } = null!;
    public string Cel { get; set; } = null!;
    public DateOnly DataRezerwacji { get; set; }
    public TimeOnly GodzinaOd { get; set; }
    public TimeOnly GodzinaDo { get; set; }
}

