using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class ApplicationDbContext :
    IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Sala> Sale { get; set; } = null!;
    public DbSet<Alergen> Alergeny { get; set; } = null!;
    public DbSet<Dieta> Diety { get; set; } = null!;
    public DbSet<Zajecie> Zajecia { get; set; } = null!;
    public DbSet<Posilek> Posilki { get; set; } = null!;
    public DbSet<PomocDydaktyczna> PomoceDydaktyczne { get; set; } = null!;
    public DbSet<PosilekAlergen> PosilekAlergeny { get; set; } = null!;
    public DbSet<Pracownik> Pracownicy { get; set; } = null!;
    public DbSet<Rodzic> Rodzice { get; set; } = null!;
    public DbSet<Grupa> Grupy { get; set; } = null!;
    public DbSet<WydarzenieKalendarz> WydarzeniaKalendarza { get; set; } = null!;
    public DbSet<GrafikPracy> GrafikPracy { get; set; } = null!;
    public DbSet<NieobecnoscPracownika> NieobecnosciPracownikow { get; set; } = null!;
    public DbSet<NadgodzinyPracownika> NadgodzinyPracownikow { get; set; } = null!;
    public DbSet<BadanieSzkoleniePracownika> BadaniaSzkoleniaPracownikow { get; set; } = null!;
    public DbSet<RezerwacjaSali> RezerwacjeSal { get; set; } = null!;
    public DbSet<RezerwacjaPomocy> RezerwacjePomocyDydaktycznych { get; set; } = null!;
    public DbSet<UsterkaPomocy> UsterkiPomocyDydaktycznych { get; set; } = null!;
    public DbSet<Dziecko> Dzieci { get; set; } = null!;
    public DbSet<PlanZajec> PlanyZajec { get; set; } = null!;
    public DbSet<DziennikZajec> DziennikiZajec { get; set; } = null!;
    public DbSet<PlanDniaGrupy> PlanyDniaGrupy { get; set; } = null!;
    public DbSet<Ogloszenie> Ogloszenia { get; set; } = null!;
    public DbSet<RodzicDziecko> RodzicDziecko { get; set; } = null!;
    public DbSet<OsobaUpowaznionaOdbior> OsobyUpowaznioneOdbior { get; set; } = null!;
    public DbSet<DzieckoAlergen> DzieckoAlergeny { get; set; } = null!;
    public DbSet<DzieckoDieta> DzieckoDiety { get; set; } = null!;
    public DbSet<OdwolaniePosilku> OdwolaniaPosilkow { get; set; } = null!;
    public DbSet<Incydent> Incydenty { get; set; } = null!;
    public DbSet<PrzeciwskazanieMedyczne> PrzeciwwskazaniaMedyczne { get; set; } = null!;
    public DbSet<LekDziecka> LekiDzieci { get; set; } = null!;
    public DbSet<KartaObserwacjiPedagogicznej> KartyObserwacjiPedagogicznej { get; set; } = null!;
    public DbSet<DzieckoPlanZajec> DzieckoPlanyZajec { get; set; } = null!;
    public DbSet<Obecnosc> Obecnosci { get; set; } = null!;
    public DbSet<RejestrPodaniaLekow> RejestryPodaniaLekow { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        ConfigureRequiredProperties(builder);
        ConfigureCompositeKeys(builder);
        ConfigureRefreshTokens(builder);

        foreach (var foreignKey in builder.Model.GetEntityTypes()
                     .SelectMany(entityType => entityType.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
        }
    }

    private static void ConfigureRequiredProperties(ModelBuilder builder)
    {
        builder.Entity<Sala>().Property(e => e.Nazwa).HasMaxLength(100);
        builder.Entity<Sala>().Property(e => e.TypSali).HasMaxLength(50).HasDefaultValue("dydaktyczna");
        builder.Entity<Alergen>().Property(e => e.Nazwa).HasMaxLength(100);
        builder.Entity<Dieta>().Property(e => e.Nazwa).HasMaxLength(100);
        builder.Entity<Zajecie>().Property(e => e.Nazwa).HasMaxLength(150);
        builder.Entity<Zajecie>().Property(e => e.TypZajec).HasMaxLength(50).HasDefaultValue("podstawowe");
        builder.Entity<Posilek>().Property(e => e.Nazwa).HasMaxLength(150);
        builder.Entity<Posilek>().Property(e => e.Opis).HasColumnType("nvarchar(max)");
        builder.Entity<PomocDydaktyczna>().Property(e => e.Nazwa).HasMaxLength(200);
        builder.Entity<PomocDydaktyczna>().Property(e => e.Typ).HasMaxLength(100);
        builder.Entity<PomocDydaktyczna>().Property(e => e.StanIlosciowy).HasDefaultValue(1);
        builder.Entity<Pracownik>().Property(e => e.Imie).HasMaxLength(100);
        builder.Entity<Pracownik>().Property(e => e.Nazwisko).HasMaxLength(100);
        builder.Entity<Pracownik>().HasIndex(e => e.UzytkownikId).IsUnique();
        builder.Entity<Rodzic>().Property(e => e.Imie).HasMaxLength(100);
        builder.Entity<Rodzic>().Property(e => e.Nazwisko).HasMaxLength(100);
        builder.Entity<Rodzic>().Property(e => e.KodPinOdbior).HasMaxLength(10);
        builder.Entity<Rodzic>().HasIndex(e => e.UzytkownikId).IsUnique();
        builder.Entity<Grupa>().Property(e => e.Nazwa).HasMaxLength(100);
        builder.Entity<Grupa>().Property(e => e.WiekDzieci).HasMaxLength(50);
        builder.Entity<WydarzenieKalendarz>().Property(e => e.Nazwa).HasMaxLength(255);
        builder.Entity<WydarzenieKalendarz>().Property(e => e.Opis).HasColumnType("nvarchar(max)");
        builder.Entity<NadgodzinyPracownika>().Property(e => e.IloscGodzin).HasPrecision(5, 2);
        builder.Entity<NadgodzinyPracownika>().Property(e => e.Powod).HasMaxLength(255);
        builder.Entity<BadanieSzkoleniePracownika>().Property(e => e.Typ).HasMaxLength(100);
        builder.Entity<RezerwacjaSali>().Property(e => e.Cel).HasMaxLength(255);
        builder.Entity<UsterkaPomocy>().Property(e => e.Opis).HasColumnType("nvarchar(max)");
        builder.Entity<UsterkaPomocy>().Property(e => e.Status).HasMaxLength(50).HasDefaultValue("Zgłoszona");
        builder.Entity<UsterkaPomocy>().Property(e => e.DataZgloszenia).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Entity<Dziecko>().Property(e => e.Imie).HasMaxLength(100);
        builder.Entity<Dziecko>().Property(e => e.Nazwisko).HasMaxLength(100);
        builder.Entity<DziennikZajec>().Property(e => e.Temat).HasMaxLength(255);
        builder.Entity<DziennikZajec>().Property(e => e.PodstawaProgramowa).HasColumnType("nvarchar(max)");
        builder.Entity<PlanDniaGrupy>().Property(e => e.Czynnosc).HasMaxLength(255);
        builder.Entity<Ogloszenie>().Property(e => e.Tytul).HasMaxLength(255);
        builder.Entity<Ogloszenie>().Property(e => e.Tresc).HasColumnType("nvarchar(max)");
        builder.Entity<Ogloszenie>().Property(e => e.DataPublikacji).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Entity<OsobaUpowaznionaOdbior>().Property(e => e.Imie).HasMaxLength(100);
        builder.Entity<OsobaUpowaznionaOdbior>().Property(e => e.Nazwisko).HasMaxLength(100);
        builder.Entity<OsobaUpowaznionaOdbior>().Property(e => e.JednorazowyKodPin).HasMaxLength(10);
        builder.Entity<Incydent>().Property(e => e.Powod).HasColumnType("nvarchar(max)");
        builder.Entity<Incydent>().Property(e => e.TypIncydentu).HasMaxLength(100).HasDefaultValue("zdrowotny");
        builder.Entity<Incydent>().Property(e => e.DataZgloszenia).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Entity<Incydent>().Property(e => e.ProtokolBhP).HasDefaultValue(false);
        builder.Entity<PrzeciwskazanieMedyczne>().Property(e => e.Opis).HasColumnType("nvarchar(max)");
        builder.Entity<LekDziecka>().Property(e => e.NazwaLeku).HasMaxLength(150);
        builder.Entity<LekDziecka>().Property(e => e.Dawka).HasMaxLength(100);
        builder.Entity<LekDziecka>().Property(e => e.Uwagi).HasColumnType("nvarchar(max)");
        builder.Entity<LekDziecka>().Property(e => e.ZgodaRodzica).HasDefaultValue(true);
        builder.Entity<KartaObserwacjiPedagogicznej>().Property(e => e.Specjalizacja).HasMaxLength(100);
        builder.Entity<KartaObserwacjiPedagogicznej>().Property(e => e.Notatka).HasColumnType("nvarchar(max)");
        builder.Entity<KartaObserwacjiPedagogicznej>().Property(e => e.DataWpisu).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Entity<Obecnosc>().Property(e => e.Status).HasMaxLength(50);
        builder.Entity<Obecnosc>().Property(e => e.MetodaRejestracji).HasMaxLength(50);
        builder.Entity<RejestrPodaniaLekow>().Property(e => e.DataPodania).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Entity<RejestrPodaniaLekow>().Property(e => e.PotwierdzeniePodpisu).HasDefaultValue(true);
    }

    private static void ConfigureCompositeKeys(ModelBuilder builder)
    {
        builder.Entity<PosilekAlergen>()
            .HasKey(entity => new { entity.PosilekId, entity.AlergenId });
        builder.Entity<RodzicDziecko>()
            .HasKey(entity => new { entity.RodzicId, entity.DzieckoId });
        builder.Entity<DzieckoAlergen>()
            .HasKey(entity => new { entity.DzieckoId, entity.AlergenId });
        builder.Entity<DzieckoDieta>()
            .HasKey(entity => new { entity.DzieckoId, entity.DietaId });
        builder.Entity<DzieckoPlanZajec>()
            .HasKey(entity => new { entity.DzieckoId, entity.PlanZajecId });
    }

    private static void ConfigureRefreshTokens(ModelBuilder builder)
    {
        builder.Entity<RefreshToken>(entity =>
        {
            entity.HasIndex(token => token.Token).IsUnique();
            entity.Property(token => token.Token).IsRequired().HasMaxLength(128);
            entity.Property(token => token.CreatedAtUtc).IsRequired();
            entity.Property(token => token.ExpiresAtUtc).IsRequired();

            entity.HasOne(token => token.User)
                .WithMany()
                .HasForeignKey(token => token.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
