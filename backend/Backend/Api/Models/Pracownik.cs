namespace Api.Models;

public class Pracownik
{
    public int Id { get; set; }
    public int UzytkownikId { get; set; }
    public ApplicationUser Uzytkownik { get; set; } = null!;
    public string Imie { get; set; } = null!;
    public string Nazwisko { get; set; } = null!;
    public ICollection<Grupa> Grupy { get; set; } = new List<Grupa>();
}

