using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class GrupaDto
{
    public int Id { get; set; }
    [Required]
    public string Nazwa { get; set; } = null!;
    [Required]
    public string WiekDzieci { get; set; } = null!;
    public int WychowawcaId { get; set; }
    public int SalaId { get; set; }
}
