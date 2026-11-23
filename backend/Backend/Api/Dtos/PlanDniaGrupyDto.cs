using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class PlanDniaGrupyDto
{
    public int Id { get; set; }
    public int GrupaId { get; set; }
    public TimeOnly GodzinaOd { get; set; }
    public TimeOnly GodzinaDo { get; set; }
    [Required]
    public string Czynnosc { get; set; } = null!;
}
