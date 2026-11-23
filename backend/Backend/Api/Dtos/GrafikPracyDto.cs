using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class GrafikPracyDto
{
    public int Id { get; set; }
    public int PracownikId { get; set; }
    public DateOnly Data { get; set; }
    public TimeOnly GodzinaRozpoczecia { get; set; }
    public TimeOnly GodzinaZakonczenia { get; set; }
}
