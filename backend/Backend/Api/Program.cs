
using Api.Data;
using Api.Models;
using Api.Services;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Api
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            EnvironmentFileLoader.Load(AppContext.BaseDirectory);
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Wprowadź token JWT."
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("SqlServer"),
                    sqlServerOptions => sqlServerOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            builder.Services
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<JwtSettings>(
                builder.Configuration.GetSection(JwtSettings.SectionName));

            var jwtSettings = builder.Configuration
                .GetSection(JwtSettings.SectionName)
                .Get<JwtSettings>()
                ?? throw new InvalidOperationException("Brak konfiguracji JwtSettings.");

            if (string.IsNullOrWhiteSpace(jwtSettings.SecretKey) ||
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey).Length < 32)
            {
                throw new InvalidOperationException(
                    "JwtSettings:SecretKey musi mieć co najmniej 32 bajty.");
            }

            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            builder.Services.AddScoped<ISalaService, SalaService>();
            builder.Services.AddScoped<IAlergenService, AlergenService>();
            builder.Services.AddScoped<IDietaService, DietaService>();
            builder.Services.AddScoped<IZajecieService, ZajecieService>();
            builder.Services.AddScoped<IPosilekService, PosilekService>();
            builder.Services.AddScoped<IPomocDydaktycznaService, PomocDydaktycznaService>();
            builder.Services.AddScoped<IPosilekAlergenService, PosilekAlergenService>();
            builder.Services.AddScoped<IPracownikService, PracownikService>();
            builder.Services.AddScoped<IRodzicService, RodzicService>();
            builder.Services.AddScoped<IGrupaService, GrupaService>();
            builder.Services.AddScoped<IWydarzenieKalendarzService, WydarzenieKalendarzService>();
            builder.Services.AddScoped<IGrafikPracyService, GrafikPracyService>();
            builder.Services.AddScoped<INieobecnoscPracownikaService, NieobecnoscPracownikaService>();
            builder.Services.AddScoped<INadgodzinyPracownikaService, NadgodzinyPracownikaService>();
            builder.Services.AddScoped<IBadanieSzkoleniePracownikaService, BadanieSzkoleniePracownikaService>();
            builder.Services.AddScoped<IRezerwacjaSaliService, RezerwacjaSaliService>();
            builder.Services.AddScoped<IRezerwacjaPomocyService, RezerwacjaPomocyService>();
            builder.Services.AddScoped<IUsterkaPomocyService, UsterkaPomocyService>();
            builder.Services.AddScoped<IDzieckoService, DzieckoService>();
            builder.Services.AddScoped<IPlanZajecService, PlanZajecService>();
            builder.Services.AddScoped<IDziennikZajecService, DziennikZajecService>();
            builder.Services.AddScoped<IPlanDniaGrupyService, PlanDniaGrupyService>();
            builder.Services.AddScoped<IOgloszenieService, OgloszenieService>();
            builder.Services.AddScoped<IRodzicDzieckoService, RodzicDzieckoService>();
            builder.Services.AddScoped<IOsobaUpowaznionaOdbiorService, OsobaUpowaznionaOdbiorService>();
            builder.Services.AddScoped<IDzieckoAlergenService, DzieckoAlergenService>();
            builder.Services.AddScoped<IDzieckoDietaService, DzieckoDietaService>();
            builder.Services.AddScoped<IOdwolaniePosilkuService, OdwolaniePosilkuService>();
            builder.Services.AddScoped<IIncydentService, IncydentService>();
            builder.Services.AddScoped<IPrzeciwskazanieMedyczneService, PrzeciwskazanieMedyczneService>();
            builder.Services.AddScoped<ILekDzieckaService, LekDzieckaService>();
            builder.Services.AddScoped<IKartaObserwacjiPedagogicznejService, KartaObserwacjiPedagogicznejService>();
            builder.Services.AddScoped<IDzieckoPlanZajecService, DzieckoPlanZajecService>();
            builder.Services.AddScoped<IObecnoscService, ObecnoscService>();
            builder.Services.AddScoped<IRejestrPodaniaLekowService, RejestrPodaniaLekowService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                using var scope = app.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                IdentityDataSeeder.SeedAsync(scope.ServiceProvider, builder.Configuration)
                    .GetAwaiter()
                    .GetResult();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
