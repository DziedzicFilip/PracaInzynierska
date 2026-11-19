using Microsoft.AspNetCore.Identity;

namespace Api.Models;

public class ApplicationUser : IdentityUser<int>
{
    public Pracownik? Pracownik { get; set; }
    public Rodzic? Rodzic { get; set; }
}
