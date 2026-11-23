using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class RezerwacjaSaliDto
{
    public int Id { get; set; }
    public int SalaId { get; set; }
    public int PracownikId { get; set; }
    [Required]
    public string Cel { get; set; } = null!;
    public DateOnly DataRezerwacji { get; set; }
    public TimeOnly GodzinaOd { get; set; }
    public TimeOnly GodzinaDo { get; set; }
}
