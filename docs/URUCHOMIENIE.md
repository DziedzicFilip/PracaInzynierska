# Uruchomienie aplikacji i bazy danych

## Wymagania

Należy zainstalować:

- .NET 8 SDK,
- Microsoft SQL Server albo SQL Server Express,
- SQL Server Management Studio (SSMS),
- narzędzie EF Core CLI.

Sprawdzenie wymaganych narzędzi:

```powershell
dotnet --version
dotnet ef --version
```

Jeśli polecenie `dotnet ef --version` zwraca komunikat o braku narzędzia:

```powershell
dotnet tool install --global dotnet-ef --version 8.*
```

Po instalacji może być konieczne ponowne otwarcie terminala.

## 1. Utworzenie instancji SQL Server

1. Zainstaluj SQL Server lub SQL Server Express.
2. Podczas instalacji włącz usługę SQL Server.
3. Zapisz nazwę instancji widoczną w SSMS, na przykład `localhost` albo `localhost\SQLEXPRESS`.
4. Otwórz SSMS i połącz się z instancją.
5. Upewnij się, że użytkownik Windows ma uprawnienia do tworzenia baz danych.

Dla lokalnego SQL Server Express domyślne połączenie może mieć postać:

```text
Server=localhost\SQLEXPRESS;Database=PracaInzynierska;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true
```

## 2. Konfiguracja połączenia

Połączenie jest skonfigurowane w:

```text
backend/Backend/Api/appsettings.Development.json
```

Domyślna wartość dla instancji domyślnej SQL Server:

```json
{
	"ConnectionStrings": {
		"SqlServer": "Server=localhost;Database=PracaInzynierska;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
	}
}
```

Dla SQL Server Express zmień wartość `Server` na:

```json
"SqlServer": "Server=localhost\\SQLEXPRESS;Database=PracaInzynierska;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

W konfiguracji JSON ukośniki są zapisane jako `\\`.

## 3. Przygotowanie projektu

Przejdź do katalogu głównego repozytorium i pobierz zależności:

```powershell
cd D:\Studia\UKEN\PracaInz\PracaInzynierska
dotnet restore backend\Backend\Backend.sln
```

## 4. Utworzenie bazy i migracja schematu

Nazwa bazy `PracaInzynierska` nie musi być wcześniej utworzona. EF Core Code First utworzy ją wraz z tabelami.

Zastosuj migrację z katalogu głównego:

```powershell
dotnet ef database update --project backend\Backend\Api\Api.csproj --startup-project backend\Backend\Api\Api.csproj
```

Polecenie:

- tworzy bazę, jeśli nie istnieje,
- tworzy tabelę historii migracji,
- tworzy tabele domenowe,
- tworzy tabele ASP.NET Core Identity.

Aplikacja w środowisku Development wykonuje migrację automatycznie podczas uruchamiania.

## 5. Uruchomienie programu

Uruchom API z katalogu głównego:

```powershell
dotnet run --project backend\Backend\Api\Api.csproj
```

Można również uruchomić go z katalogu projektu:

```powershell
cd backend\Backend\Api
dotnet run
```

Po uruchomieniu Swagger powinien być dostępny pod adresem:

```text
http://localhost:5155/swagger
```

Adres pochodzi z profilu `http` w `Properties/launchSettings.json`.

## 6. Sprawdzenie bazy w SSMS

Połącz się w SSMS z poprawną instancją i odśwież sekcję **Bazy danych**.

Powinna być widoczna baza `PracaInzynierska`. Najczęściej zawiera ona:

- tabele ASP.NET Core Identity, np. `AspNetUsers` i `AspNetRoles`,
- tabele modeli aplikacji, np. `Sale`, `Grupy` i `Dzieci`,
- tabelę `__EFMigrationsHistory`.

## Ponowne utworzenie bazy

Jeśli baza została utworzona niepoprawnie i trzeba ją zbudować ponownie, wykonaj w SSMS:

```sql
USE master;
ALTER DATABASE [PracaInzynierska] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE [PracaInzynierska];
```

Po usunięciu bazy uruchom ponownie migrację:

```powershell
dotnet ef database update --project backend\Backend\Api\Api.csproj --startup-project backend\Backend\Api\Api.csproj
```

## Rozwiązywanie problemów

### Plik Api.exe jest zablokowany

Jeśli dotnet run zgłasza, że plik `Api.exe` jest używany przez proces `Api`, zatrzymaj działającą aplikację klawiszami `Ctrl+C` lub sprawdź procesy:

```powershell
Get-Process -Name Api
Stop-Process -Id <PID> -Force
dotnet run --project backend\Backend\Api\Api.csproj
```

Nie należy używać `Stop-Process -Name`.

### Nie można połączyć się z SQL Server

Sprawdź:

- czy usługa SQL Server działa,
- czy nazwa instancji została wpisana poprawnie,
- czy w SSMS otwarta instancja odpowiada wartości `Server` w `appsettings.Development.json`,
- czy użytkownik Windows ma uprawnienia do tworzenia i używania baz danych.

### Migracja zakończyła się błędem

Najpierw sprawdź połączenie w SSMS, a następnie:

```powershell
dotnet ef migrations list --project backend\Backend\Api\Api.csproj --startup-project backend\Backend\Api\Api.csproj
dotnet ef database update --project backend\Backend\Api\Api.csproj --startup-project backend\Backend\Api\Api.csproj
```
