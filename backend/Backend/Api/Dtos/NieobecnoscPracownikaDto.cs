using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class NieobecnoscPracownikaDto
{
    public int Id { get; set; }
    public int PracownikId { get; set; }
    public int? ZastepcaId { get; set; }
    [Required]
    public string Powod { get; set; } = null!;
    public DateOnly DataOd { get; set; }
    public DateOnly DataDo { get; set; }
}
