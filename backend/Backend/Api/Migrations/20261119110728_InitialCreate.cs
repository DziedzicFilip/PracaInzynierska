using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alergeny",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alergeny", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diety",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diety", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posilki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posilki", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TypSali = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "dydaktyczna")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zajecia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TypZajec = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "podstawowe")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zajecia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pracownicy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UzytkownikId = table.Column<int>(type: "int", nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pracownicy_AspNetUsers_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rodzice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UzytkownikId = table.Column<int>(type: "int", nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KodPinOdbior = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rodzice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rodzice_AspNetUsers_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PosilekAlergeny",
                columns: table => new
                {
                    PosilekId = table.Column<int>(type: "int", nullable: false),
                    AlergenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosilekAlergeny", x => new { x.PosilekId, x.AlergenId });
                    table.ForeignKey(
                        name: "FK_PosilekAlergeny_Alergeny_AlergenId",
                        column: x => x.AlergenId,
                        principalTable: "Alergeny",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PosilekAlergeny_Posilki_PosilekId",
                        column: x => x.PosilekId,
                        principalTable: "Posilki",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PomoceDydaktyczne",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Typ = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StanIlosciowy = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    SalaPrzypisanaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PomoceDydaktyczne", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PomoceDydaktyczne_Sale_SalaPrzypisanaId",
                        column: x => x.SalaPrzypisanaId,
                        principalTable: "Sale",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BadaniaSzkoleniaPracownikow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    Typ = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataWaznosci = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadaniaSzkoleniaPracownikow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BadaniaSzkoleniaPracownikow_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GrafikPracy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    GodzinaRozpoczecia = table.Column<TimeOnly>(type: "time", nullable: false),
                    GodzinaZakonczenia = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrafikPracy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrafikPracy_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Grupy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WiekDzieci = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WychowawcaId = table.Column<int>(type: "int", nullable: false),
                    SalaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grupy_Pracownicy_WychowawcaId",
                        column: x => x.WychowawcaId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Grupy_Sale_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sale",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NadgodzinyPracownikow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    IloscGodzin = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Powod = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NadgodzinyPracownikow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NadgodzinyPracownikow_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NieobecnosciPracownikow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    ZastepcaId = table.Column<int>(type: "int", nullable: true),
                    Powod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataOd = table.Column<DateOnly>(type: "date", nullable: false),
                    DataDo = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NieobecnosciPracownikow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NieobecnosciPracownikow_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NieobecnosciPracownikow_Pracownicy_ZastepcaId",
                        column: x => x.ZastepcaId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RezerwacjeSal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaId = table.Column<int>(type: "int", nullable: false),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    Cel = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DataRezerwacji = table.Column<DateOnly>(type: "date", nullable: false),
                    GodzinaOd = table.Column<TimeOnly>(type: "time", nullable: false),
                    GodzinaDo = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezerwacjeSal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RezerwacjeSal_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RezerwacjeSal_Sale_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sale",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WydarzeniaKalendarza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataWydarzenia = table.Column<DateOnly>(type: "date", nullable: false),
                    GodzinaRozpoczecia = table.Column<TimeOnly>(type: "time", nullable: true),
                    GodzinaZakonczenia = table.Column<TimeOnly>(type: "time", nullable: true),
                    PracownikOdpowiedzialnyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WydarzeniaKalendarza", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WydarzeniaKalendarza_Pracownicy_PracownikOdpowiedzialnyId",
                        column: x => x.PracownikOdpowiedzialnyId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RezerwacjePomocyDydaktycznych",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PomocId = table.Column<int>(type: "int", nullable: false),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    DataOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezerwacjePomocyDydaktycznych", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RezerwacjePomocyDydaktycznych_PomoceDydaktyczne_PomocId",
                        column: x => x.PomocId,
                        principalTable: "PomoceDydaktyczne",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RezerwacjePomocyDydaktycznych_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsterkiPomocyDydaktycznych",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PomocId = table.Column<int>(type: "int", nullable: false),
                    PracownikZglaszajacyId = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataZgloszenia = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Zgłoszona")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsterkiPomocyDydaktycznych", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsterkiPomocyDydaktycznych_PomoceDydaktyczne_PomocId",
                        column: x => x.PomocId,
                        principalTable: "PomoceDydaktyczne",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsterkiPomocyDydaktycznych_Pracownicy_PracownikZglaszajacyId",
                        column: x => x.PracownikZglaszajacyId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Dzieci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataUrodzenia = table.Column<DateOnly>(type: "date", nullable: false),
                    GrupaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dzieci", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dzieci_Grupy_GrupaId",
                        column: x => x.GrupaId,
                        principalTable: "Grupy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DziennikiZajec",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrupaId = table.Column<int>(type: "int", nullable: false),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    Temat = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PodstawaProgramowa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DziennikiZajec", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DziennikiZajec_Grupy_GrupaId",
                        column: x => x.GrupaId,
                        principalTable: "Grupy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DziennikiZajec_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ogloszenia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutorId = table.Column<int>(type: "int", nullable: false),
                    GrupaId = table.Column<int>(type: "int", nullable: true),
                    Tytul = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Tresc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPublikacji = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogloszenia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ogloszenia_Grupy_GrupaId",
                        column: x => x.GrupaId,
                        principalTable: "Grupy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ogloszenia_Pracownicy_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlanyDniaGrupy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrupaId = table.Column<int>(type: "int", nullable: false),
                    GodzinaOd = table.Column<TimeOnly>(type: "time", nullable: false),
                    GodzinaDo = table.Column<TimeOnly>(type: "time", nullable: false),
                    Czynnosc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanyDniaGrupy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanyDniaGrupy_Grupy_GrupaId",
                        column: x => x.GrupaId,
                        principalTable: "Grupy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlanyZajec",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZajecieId = table.Column<int>(type: "int", nullable: false),
                    GrupaId = table.Column<int>(type: "int", nullable: true),
                    SalaId = table.Column<int>(type: "int", nullable: false),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    DzienTygodnia = table.Column<int>(type: "int", nullable: false),
                    CzasRozpoczecia = table.Column<TimeOnly>(type: "time", nullable: false),
                    CzasZakonczenia = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanyZajec", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanyZajec_Grupy_GrupaId",
                        column: x => x.GrupaId,
                        principalTable: "Grupy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlanyZajec_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlanyZajec_Sale_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sale",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlanyZajec_Zajecia_ZajecieId",
                        column: x => x.ZajecieId,
                        principalTable: "Zajecia",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DzieckoAlergeny",
                columns: table => new
                {
                    DzieckoId = table.Column<int>(type: "int", nullable: false),
                    AlergenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DzieckoAlergeny", x => new { x.DzieckoId, x.AlergenId });
                    table.ForeignKey(
                        name: "FK_DzieckoAlergeny_Alergeny_AlergenId",
                        column: x => x.AlergenId,
                        principalTable: "Alergeny",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DzieckoAlergeny_Dzieci_DzieckoId",
                        column: x => x.DzieckoId,
                        principalTable: "Dzieci",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DzieckoDiety",
                columns: table => new
                {
                    DzieckoId = table.Column<int>(type: "int", nullable: false),
                    DietaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DzieckoDiety", x => new { x.DzieckoId, x.DietaId });
                    table.ForeignKey(
                        name: "FK_DzieckoDiety_Diety_DietaId",
                        column: x => x.DietaId,
                        principalTable: "Diety",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DzieckoDiety_Dzieci_DzieckoId",
                        column: x => x.DzieckoId,
                        principalTable: "Dzieci",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Incydenty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DzieckoId = table.Column<int>(type: "int", nullable: false),
                    RodzicId = table.Column<int>(type: "int", nullable: true),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    Powod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypIncydentu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "zdrowotny"),
                    DataZgloszenia = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ProtokolBhP = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incydenty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incydenty_Dzieci_DzieckoId",
                        column: x => x.DzieckoId,
                        principalTable: "Dzieci",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Incydenty_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Incydenty_Rodzice_RodzicId",
                        column: x => x.RodzicId,
                        principalTable: "Rodzice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KartyObserwacjiPedagogicznej",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DzieckoId = table.Column<int>(type: "int", nullable: false),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    Specjalizacja = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Notatka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataWpisu = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KartyObserwacjiPedagogicznej", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KartyObserwacjiPedagogicznej_Dzieci_DzieckoId",
                        column: x => x.DzieckoId,
                        principalTable: "Dzieci",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KartyObserwacjiPedagogicznej_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LekiDzieci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DzieckoId = table.Column<int>(type: "int", nullable: false),
                    NazwaLeku = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Dawka = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GodzinaPodania = table.Column<TimeOnly>(type: "time", nullable: false),
                    DataOd = table.Column<DateOnly>(type: "date", nullable: true),
                    DataDo = table.Column<DateOnly>(type: "date", nullable: true),
                    Uwagi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZgodaRodzica = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LekiDzieci", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LekiDzieci_Dzieci_DzieckoId",
                        column: x => x.DzieckoId,
                        principalTable: "Dzieci",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OdwolaniaPosilkow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DzieckoId = table.Column<int>(type: "int", nullable: false),
                    DataPosilku = table.Column<DateOnly>(type: "date", nullable: false),
                    DataZgloszenia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdwolaniaPosilkow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OdwolaniaPosilkow_Dzieci_DzieckoId",
                        column: x => x.DzieckoId,
                        principalTable: "Dzieci",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OsobyUpowaznioneOdbior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DzieckoId = table.Column<int>(type: "int", nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    JednorazowyKodPin = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsobyUpowaznioneOdbior", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OsobyUpowaznioneOdbior_Dzieci_DzieckoId",
                        column: x => x.DzieckoId,
                        principalTable: "Dzieci",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PrzeciwwskazaniaMedyczne",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DzieckoId = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrzeciwwskazaniaMedyczne", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrzeciwwskazaniaMedyczne_Dzieci_DzieckoId",
                        column: x => x.DzieckoId,
                        principalTable: "Dzieci",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RodzicDziecko",
                columns: table => new
                {
                    RodzicId = table.Column<int>(type: "int", nullable: false),
                    DzieckoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RodzicDziecko", x => new { x.RodzicId, x.DzieckoId });
                    table.ForeignKey(
                        name: "FK_RodzicDziecko_Dzieci_DzieckoId",
                        column: x => x.DzieckoId,
                        principalTable: "Dzieci",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RodzicDziecko_Rodzice_RodzicId",
                        column: x => x.RodzicId,
                        principalTable: "Rodzice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DzieckoPlanyZajec",
                columns: table => new
                {
                    DzieckoId = table.Column<int>(type: "int", nullable: false),
                    PlanZajecId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DzieckoPlanyZajec", x => new { x.DzieckoId, x.PlanZajecId });
                    table.ForeignKey(
                        name: "FK_DzieckoPlanyZajec_Dzieci_DzieckoId",
                        column: x => x.DzieckoId,
                        principalTable: "Dzieci",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DzieckoPlanyZajec_PlanyZajec_PlanZajecId",
                        column: x => x.PlanZajecId,
                        principalTable: "PlanyZajec",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RejestryPodaniaLekow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LekId = table.Column<int>(type: "int", nullable: false),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    DataPodania = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    PotwierdzeniePodpisu = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RejestryPodaniaLekow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RejestryPodaniaLekow_LekiDzieci_LekId",
                        column: x => x.LekId,
                        principalTable: "LekiDzieci",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RejestryPodaniaLekow_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Obecnosci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DzieckoId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GodzinaPrzyjscia = table.Column<TimeOnly>(type: "time", nullable: true),
                    GodzinaOdbioru = table.Column<TimeOnly>(type: "time", nullable: true),
                    RodzicOdbierajacyId = table.Column<int>(type: "int", nullable: true),
                    OsobaUpowaznionaId = table.Column<int>(type: "int", nullable: true),
                    MetodaRejestracji = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obecnosci", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Obecnosci_Dzieci_DzieckoId",
                        column: x => x.DzieckoId,
                        principalTable: "Dzieci",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Obecnosci_OsobyUpowaznioneOdbior_OsobaUpowaznionaId",
                        column: x => x.OsobaUpowaznionaId,
                        principalTable: "OsobyUpowaznioneOdbior",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Obecnosci_Rodzice_RodzicOdbierajacyId",
                        column: x => x.RodzicOdbierajacyId,
                        principalTable: "Rodzice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BadaniaSzkoleniaPracownikow_PracownikId",
                table: "BadaniaSzkoleniaPracownikow",
                column: "PracownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Dzieci_GrupaId",
                table: "Dzieci",
                column: "GrupaId");

            migrationBuilder.CreateIndex(
                name: "IX_DzieckoAlergeny_AlergenId",
                table: "DzieckoAlergeny",
                column: "AlergenId");

            migrationBuilder.CreateIndex(
                name: "IX_DzieckoDiety_DietaId",
                table: "DzieckoDiety",
                column: "DietaId");

            migrationBuilder.CreateIndex(
                name: "IX_DzieckoPlanyZajec_PlanZajecId",
                table: "DzieckoPlanyZajec",
                column: "PlanZajecId");

            migrationBuilder.CreateIndex(
                name: "IX_DziennikiZajec_GrupaId",
                table: "DziennikiZajec",
                column: "GrupaId");

            migrationBuilder.CreateIndex(
                name: "IX_DziennikiZajec_PracownikId",
                table: "DziennikiZajec",
                column: "PracownikId");

            migrationBuilder.CreateIndex(
                name: "IX_GrafikPracy_PracownikId",
                table: "GrafikPracy",
                column: "PracownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupy_SalaId",
                table: "Grupy",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupy_WychowawcaId",
                table: "Grupy",
                column: "WychowawcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Incydenty_DzieckoId",
                table: "Incydenty",
                column: "DzieckoId");

            migrationBuilder.CreateIndex(
                name: "IX_Incydenty_PracownikId",
                table: "Incydenty",
                column: "PracownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Incydenty_RodzicId",
                table: "Incydenty",
                column: "RodzicId");

            migrationBuilder.CreateIndex(
                name: "IX_KartyObserwacjiPedagogicznej_DzieckoId",
                table: "KartyObserwacjiPedagogicznej",
                column: "DzieckoId");

            migrationBuilder.CreateIndex(
                name: "IX_KartyObserwacjiPedagogicznej_PracownikId",
                table: "KartyObserwacjiPedagogicznej",
                column: "PracownikId");

            migrationBuilder.CreateIndex(
                name: "IX_LekiDzieci_DzieckoId",
                table: "LekiDzieci",
                column: "DzieckoId");

            migrationBuilder.CreateIndex(
                name: "IX_NadgodzinyPracownikow_PracownikId",
                table: "NadgodzinyPracownikow",
                column: "PracownikId");

            migrationBuilder.CreateIndex(
                name: "IX_NieobecnosciPracownikow_PracownikId",
                table: "NieobecnosciPracownikow",
                column: "PracownikId");

            migrationBuilder.CreateIndex(
                name: "IX_NieobecnosciPracownikow_ZastepcaId",
                table: "NieobecnosciPracownikow",
                column: "ZastepcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Obecnosci_DzieckoId",
                table: "Obecnosci",
                column: "DzieckoId");

            migrationBuilder.CreateIndex(
                name: "IX_Obecnosci_OsobaUpowaznionaId",
                table: "Obecnosci",
                column: "OsobaUpowaznionaId");

            migrationBuilder.CreateIndex(
                name: "IX_Obecnosci_RodzicOdbierajacyId",
                table: "Obecnosci",
                column: "RodzicOdbierajacyId");

            migrationBuilder.CreateIndex(
                name: "IX_OdwolaniaPosilkow_DzieckoId",
                table: "OdwolaniaPosilkow",
                column: "DzieckoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogloszenia_AutorId",
                table: "Ogloszenia",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogloszenia_GrupaId",
                table: "Ogloszenia",
                column: "GrupaId");

            migrationBuilder.CreateIndex(
                name: "IX_OsobyUpowaznioneOdbior_DzieckoId",
                table: "OsobyUpowaznioneOdbior",
                column: "DzieckoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanyDniaGrupy_GrupaId",
                table: "PlanyDniaGrupy",
                column: "GrupaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanyZajec_GrupaId",
                table: "PlanyZajec",
                column: "GrupaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanyZajec_PracownikId",
                table: "PlanyZajec",
                column: "PracownikId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanyZajec_SalaId",
                table: "PlanyZajec",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanyZajec_ZajecieId",
                table: "PlanyZajec",
                column: "ZajecieId");

            migrationBuilder.CreateIndex(
                name: "IX_PomoceDydaktyczne_SalaPrzypisanaId",
                table: "PomoceDydaktyczne",
                column: "SalaPrzypisanaId");

            migrationBuilder.CreateIndex(
                name: "IX_PosilekAlergeny_AlergenId",
                table: "PosilekAlergeny",
                column: "AlergenId");

            migrationBuilder.CreateIndex(
                name: "IX_Pracownicy_UzytkownikId",
                table: "Pracownicy",
                column: "UzytkownikId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrzeciwwskazaniaMedyczne_DzieckoId",
                table: "PrzeciwwskazaniaMedyczne",
                column: "DzieckoId");

            migrationBuilder.CreateIndex(
                name: "IX_RejestryPodaniaLekow_LekId",
                table: "RejestryPodaniaLekow",
                column: "LekId");

            migrationBuilder.CreateIndex(
                name: "IX_RejestryPodaniaLekow_PracownikId",
                table: "RejestryPodaniaLekow",
                column: "PracownikId");

            migrationBuilder.CreateIndex(
                name: "IX_RezerwacjePomocyDydaktycznych_PomocId",
                table: "RezerwacjePomocyDydaktycznych",
                column: "PomocId");

            migrationBuilder.CreateIndex(
                name: "IX_RezerwacjePomocyDydaktycznych_PracownikId",
                table: "RezerwacjePomocyDydaktycznych",
                column: "PracownikId");

            migrationBuilder.CreateIndex(
                name: "IX_RezerwacjeSal_PracownikId",
                table: "RezerwacjeSal",
                column: "PracownikId");

            migrationBuilder.CreateIndex(
                name: "IX_RezerwacjeSal_SalaId",
                table: "RezerwacjeSal",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_RodzicDziecko_DzieckoId",
                table: "RodzicDziecko",
                column: "DzieckoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rodzice_UzytkownikId",
                table: "Rodzice",
                column: "UzytkownikId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsterkiPomocyDydaktycznych_PomocId",
                table: "UsterkiPomocyDydaktycznych",
                column: "PomocId");

            migrationBuilder.CreateIndex(
                name: "IX_UsterkiPomocyDydaktycznych_PracownikZglaszajacyId",
                table: "UsterkiPomocyDydaktycznych",
                column: "PracownikZglaszajacyId");

            migrationBuilder.CreateIndex(
                name: "IX_WydarzeniaKalendarza_PracownikOdpowiedzialnyId",
                table: "WydarzeniaKalendarza",
                column: "PracownikOdpowiedzialnyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BadaniaSzkoleniaPracownikow");

            migrationBuilder.DropTable(
                name: "DzieckoAlergeny");

            migrationBuilder.DropTable(
                name: "DzieckoDiety");

            migrationBuilder.DropTable(
                name: "DzieckoPlanyZajec");

            migrationBuilder.DropTable(
                name: "DziennikiZajec");

            migrationBuilder.DropTable(
                name: "GrafikPracy");

            migrationBuilder.DropTable(
                name: "Incydenty");

            migrationBuilder.DropTable(
                name: "KartyObserwacjiPedagogicznej");

            migrationBuilder.DropTable(
                name: "NadgodzinyPracownikow");

            migrationBuilder.DropTable(
                name: "NieobecnosciPracownikow");

            migrationBuilder.DropTable(
                name: "Obecnosci");

            migrationBuilder.DropTable(
                name: "OdwolaniaPosilkow");

            migrationBuilder.DropTable(
                name: "Ogloszenia");

            migrationBuilder.DropTable(
                name: "PlanyDniaGrupy");

            migrationBuilder.DropTable(
                name: "PosilekAlergeny");

            migrationBuilder.DropTable(
                name: "PrzeciwwskazaniaMedyczne");

            migrationBuilder.DropTable(
                name: "RejestryPodaniaLekow");

            migrationBuilder.DropTable(
                name: "RezerwacjePomocyDydaktycznych");

            migrationBuilder.DropTable(
                name: "RezerwacjeSal");

            migrationBuilder.DropTable(
                name: "RodzicDziecko");

            migrationBuilder.DropTable(
                name: "UsterkiPomocyDydaktycznych");

            migrationBuilder.DropTable(
                name: "WydarzeniaKalendarza");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Diety");

            migrationBuilder.DropTable(
                name: "PlanyZajec");

            migrationBuilder.DropTable(
                name: "OsobyUpowaznioneOdbior");

            migrationBuilder.DropTable(
                name: "Alergeny");

            migrationBuilder.DropTable(
                name: "Posilki");

            migrationBuilder.DropTable(
                name: "LekiDzieci");

            migrationBuilder.DropTable(
                name: "Rodzice");

            migrationBuilder.DropTable(
                name: "PomoceDydaktyczne");

            migrationBuilder.DropTable(
                name: "Zajecia");

            migrationBuilder.DropTable(
                name: "Dzieci");

            migrationBuilder.DropTable(
                name: "Grupy");

            migrationBuilder.DropTable(
                name: "Pracownicy");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
