using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class RejestrPodaniaLekowDto
{
    public int Id { get; set; }
    public int LekId { get; set; }
    public int PracownikId { get; set; }
    public DateTime DataPodania { get; set; }
    public bool PotwierdzeniePodpisu { get; set; }
}
