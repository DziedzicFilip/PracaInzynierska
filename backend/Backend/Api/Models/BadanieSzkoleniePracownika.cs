namespace Api.Models;

public class BadanieSzkoleniePracownika
{
    public int Id { get; set; }
    public int PracownikId { get; set; }
    public Pracownik Pracownik { get; set; } = null!;
    public string Typ { get; set; } = null!;
    public DateOnly DataWaznosci { get; set; }
}

