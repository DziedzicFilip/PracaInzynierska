# Konfiguracja i konto SuperAdministratora

## Plik `.env`

Plik `.env` znajduje się w katalogu głównym repozytorium i jest ignorowany przez Git.

Wymagane zmienne:

```text
SUPERADMIN_USERNAME=superadmin
SUPERADMIN_EMAIL=adres-email
SUPERADMIN_PASSWORD=silne-haslo
```

Bezpieczny szablon znajduje się w `.env.example`. Służy do utworzenia lokalnej konfiguracji:

```powershell
Copy-Item .env.example .env
```

Następnie należy uzupełnić własne wartości.

## Ładowanie konfiguracji

`EnvironmentFileLoader`:

1. szuka `.env` zaczynając od katalogu aplikacji,
2. przesuwa wyszukiwanie do katalogów nadrzędnych,
3. odczytuje pary `KLUCZ=WARTOSC`,
4. dodaje brakujące wartości do zmiennych środowiskowych procesu.

Zmienne ustawione bezpośrednio w środowisku mają pierwszeństwo przed wartościami z pliku.

## Seeder

`IdentityDataSeeder` jest uruchamiany podczas startu aplikacji w środowisku Development.

Tworzy:

- wszystkie role systemowe,
- konto SuperAdministratora, jeżeli nie istnieje,
- rolę `SuperAdministrator`,
- rolę `Administrator`.

Dane konta pobierane są z `SUPERADMIN_USERNAME`, `SUPERADMIN_EMAIL` i `SUPERADMIN_PASSWORD`.

Nie należy umieszczać prawdziwego hasła w `appsettings.json`, commitach ani publicznych przykładach.
