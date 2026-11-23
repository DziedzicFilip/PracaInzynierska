using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class OdwolaniePosilkuDto
{
    public int Id { get; set; }
    public int DzieckoId { get; set; }
    public DateOnly DataPosilku { get; set; }
    public DateTime DataZgloszenia { get; set; }
}
