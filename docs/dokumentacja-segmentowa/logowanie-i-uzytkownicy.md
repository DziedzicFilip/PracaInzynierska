# Logowanie i użytkownicy

## Bez publicznej rejestracji

API nie udostępnia publicznego endpointu rejestracji. Konta są tworzone:

- przez seeder podczas uruchomienia środowiska Development,
- przez administratora lub przyszły moduł administracyjny korzystający z `UserManager<ApplicationUser>`,
- bezpośrednio przez ASP.NET Core Identity w procesach administracyjnych.

Nie należy dodawać punktu końcowego rejestracji bez osobnej decyzji o zasadach tworzenia kont i przypisywania ról.

## Proces logowania

Klient wysyła e-mail i hasło do `POST /api/auth/login`.

Serwis `AuthService`:

1. wyszukuje użytkownika na podstawie adresu e-mail,
2. sprawdza hasło przez `UserManager`,
3. pobiera role przypisane użytkownikowi,
4. generuje Access Token i Refresh Token,
5. zapisuje Refresh Token w tabeli `RefreshTokens`,
6. zwraca dane użytkownika, role i obie wartości tokenów.

Nieprawidłowe dane logowania zwracają `401 Unauthorized`.
