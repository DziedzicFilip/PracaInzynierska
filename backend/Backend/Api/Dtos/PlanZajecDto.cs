using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class PlanZajecDto
{
    public int Id { get; set; }
    public int ZajecieId { get; set; }
    public int? GrupaId { get; set; }
    public int SalaId { get; set; }
    public int PracownikId { get; set; }
    public int DzienTygodnia { get; set; }
    public TimeOnly CzasRozpoczecia { get; set; }
    public TimeOnly CzasZakonczenia { get; set; }
}
