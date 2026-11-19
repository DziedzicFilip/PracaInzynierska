using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class SalaDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nazwa { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string TypSali { get; set; } = "dydaktyczna";
}
