using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class RezerwacjaPomocyDto
{
    public int Id { get; set; }
    public int PomocId { get; set; }
    public int PracownikId { get; set; }
    public DateTime DataOd { get; set; }
    public DateTime DataDo { get; set; }
}
