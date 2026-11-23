using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class BadanieSzkoleniePracownikaDto
{
    public int Id { get; set; }
    public int PracownikId { get; set; }
    [Required]
    public string Typ { get; set; } = null!;
    public DateOnly DataWaznosci { get; set; }
}
