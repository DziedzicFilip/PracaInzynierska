using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class AlergenDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nazwa { get; set; } = null!;
}
