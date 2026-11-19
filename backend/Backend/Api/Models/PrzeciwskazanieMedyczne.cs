namespace Api.Models;

public class PrzeciwskazanieMedyczne
{
    public int Id { get; set; }
    public int DzieckoId { get; set; }
    public Dziecko Dziecko { get; set; } = null!;
    public string Opis { get; set; } = null!;
}

