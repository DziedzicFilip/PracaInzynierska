using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class ZajecieDto
{
    public int Id { get; set; }
    [Required]
    public string Nazwa { get; set; } = null!;
    [Required]
    public string TypZajec { get; set; } = null!;
}
