using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class NadgodzinyPracownikaDto
{
    public int Id { get; set; }
    public int PracownikId { get; set; }
    public DateOnly Data { get; set; }
    public decimal IloscGodzin { get; set; }
    public string? Powod { get; set; }
}
