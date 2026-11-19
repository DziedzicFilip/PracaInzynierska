namespace Api.Models;

public class RodzicDziecko
{
    public int RodzicId { get; set; }
    public Rodzic Rodzic { get; set; } = null!;
    public int DzieckoId { get; set; }
    public Dziecko Dziecko { get; set; } = null!;
}

