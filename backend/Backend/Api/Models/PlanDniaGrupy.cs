namespace Api.Models;

public class PlanDniaGrupy
{
    public int Id { get; set; }
    public int GrupaId { get; set; }
    public Grupa Grupa { get; set; } = null!;
    public TimeOnly GodzinaOd { get; set; }
    public TimeOnly GodzinaDo { get; set; }
    public string Czynnosc { get; set; } = null!;
}

