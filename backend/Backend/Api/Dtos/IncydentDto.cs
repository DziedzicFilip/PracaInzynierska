using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class IncydentDto
{
    public int Id { get; set; }
    public int DzieckoId { get; set; }
    public int? RodzicId { get; set; }
    public int PracownikId { get; set; }
    [Required]
    public string Powod { get; set; } = null!;
    [Required]
    public string TypIncydentu { get; set; } = null!;
    public DateTime DataZgloszenia { get; set; }
    public bool ProtokolBhP { get; set; }
}
