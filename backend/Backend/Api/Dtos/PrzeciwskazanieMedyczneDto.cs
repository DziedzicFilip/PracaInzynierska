using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class PrzeciwskazanieMedyczneDto
{
    public int Id { get; set; }
    public int DzieckoId { get; set; }
    [Required]
    public string Opis { get; set; } = null!;
}
