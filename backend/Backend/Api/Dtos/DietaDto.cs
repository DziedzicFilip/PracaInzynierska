using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class DietaDto
{
    public int Id { get; set; }
    [Required]
    public string Nazwa { get; set; } = null!;
}
