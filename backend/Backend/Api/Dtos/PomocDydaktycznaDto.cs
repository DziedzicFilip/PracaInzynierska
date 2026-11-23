using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class PomocDydaktycznaDto
{
    public int Id { get; set; }
    [Required]
    public string Nazwa { get; set; } = null!;
    [Required]
    public string Typ { get; set; } = null!;
    public int StanIlosciowy { get; set; }
    public int? SalaPrzypisanaId { get; set; }
}
