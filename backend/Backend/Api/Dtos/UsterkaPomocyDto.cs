using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class UsterkaPomocyDto
{
    public int Id { get; set; }
    public int PomocId { get; set; }
    public int PracownikZglaszajacyId { get; set; }
    [Required]
    public string Opis { get; set; } = null!;
    public DateTime DataZgloszenia { get; set; }
    [Required]
    public string Status { get; set; } = null!;
}
