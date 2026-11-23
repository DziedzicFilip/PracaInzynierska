using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class KartaObserwacjiPedagogicznejDto
{
    public int Id { get; set; }
    public int DzieckoId { get; set; }
    public int PracownikId { get; set; }
    [Required]
    public string Specjalizacja { get; set; } = null!;
    [Required]
    public string Notatka { get; set; } = null!;
    public DateTime DataWpisu { get; set; }
}
