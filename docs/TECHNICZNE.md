# Dokumentacja techniczna

## Aktualnie używane technologie

### Backend

- .NET 8 i ASP.NET Core Web API.
- Entity Framework Core 8 Code First.
- Microsoft SQL Server.
- ASP.NET Core Identity z kluczami `int`.
- Swagger/OpenAPI do przeglądania i testowania endpointów.
- Migracje EF Core do tworzenia i aktualizacji bazy danych.
- NUnit do testów jednostkowych i integracyjnych.

### Architektura backendu

Aktualnie projekt znajduje się w aplikacji `backend/Backend/Api`.

- `Models` — modele encji i modele użytkownika oraz roli Identity.
- `Data/ApplicationDbContext.cs` — konfiguracja Entity Framework Core i zbiorów `DbSet`.
- `Migrations` — migracje bazy danych Code First.
- `Controllers` — kontrolery ASP.NET Core przyjmujące i zwracające żądania HTTP.
- `Services/Interfaces` — interfejsy usług definiujące operacje logiki aplikacji.
- `Services` — implementacje usług korzystające z `ApplicationDbContext`.
- `Program.cs` — rejestracja usług, Identity i połączenia SQL Server.

### Warstwa serwisów i interfejsów

Kontrolery nie powinny bezpośrednio wykonywać operacji na bazie danych.
Operacje są przekazywane do serwisu przez interfejs.

```csharp
builder.Services.AddScoped<ISalaService, SalaService>();
```

Implementacja serwisu została utworzona dla encji `Sala`:

- `ISalaService` — kontrakt CRUD dostępny dla kontrolera,
- `SalaService` — implementacja Entity Framework Core,
- `SaleController` — endpointy REST pod `/api/sale`.

Kontrolery uzyskują zależności przez Dependency Injection.
