using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class OsobaUpowaznionaOdbiorDto
{
    public int Id { get; set; }
    public int DzieckoId { get; set; }
    [Required]
    public string Imie { get; set; } = null!;
    [Required]
    public string Nazwisko { get; set; } = null!;
    public string? JednorazowyKodPin { get; set; }
}
