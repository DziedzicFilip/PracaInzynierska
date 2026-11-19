namespace Api.Models;

public class DziennikZajec
{
    public int Id { get; set; }
    public int GrupaId { get; set; }
    public Grupa Grupa { get; set; } = null!;
    public int PracownikId { get; set; }
    public Pracownik Pracownik { get; set; } = null!;
    public DateOnly Data { get; set; }
    public string Temat { get; set; } = null!;
    public string? PodstawaProgramowa { get; set; }
}

