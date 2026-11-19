namespace Api.Models;

public class Rodzic
{
    public int Id { get; set; }
    public int UzytkownikId { get; set; }
    public ApplicationUser Uzytkownik { get; set; } = null!;
    public string Imie { get; set; } = null!;
    public string Nazwisko { get; set; } = null!;
    public string? KodPinOdbior { get; set; }
    public ICollection<RodzicDziecko> Dzieci { get; set; } = new List<RodzicDziecko>();
}

