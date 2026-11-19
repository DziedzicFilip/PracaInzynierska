namespace Api.Models;

public class NieobecnoscPracownika
{
    public int Id { get; set; }
    public int PracownikId { get; set; }
    public Pracownik Pracownik { get; set; } = null!;
    public int? ZastepcaId { get; set; }
    public Pracownik? Zastepca { get; set; }
    public string Powod { get; set; } = null!;
    public DateOnly DataOd { get; set; }
    public DateOnly DataDo { get; set; }
}

