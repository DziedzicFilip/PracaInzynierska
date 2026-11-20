# JWT i Access Token

## Cel

Access Token przekazuje tożsamość i role użytkownika bez przechowywania sesji po stronie serwera.

Klient powinien wysyłać go w nagłówku:

```http
Authorization: Bearer <access-token>
```

## Roszczenia

Generowany token zawiera:

- `ClaimTypes.NameIdentifier` — identyfikator użytkownika,
- `ClaimTypes.Name` — nazwa użytkownika,
- `ClaimTypes.Email` — e-mail użytkownika,
- `JwtRegisteredClaimNames.Jti` — unikalny identyfikator tokenu,
- `ClaimTypes.Role` — wszystkie role przypisane użytkownikowi.

## Podpis i walidacja

Token jest podpisany algorytmem HMAC-SHA256 kluczem z `JwtSettings:SecretKey`.

ASP.NET Core weryfikuje:

- wystawcę tokenu,
- odbiorcę tokenu,
- czas ważności,
- klucz podpisu.

`ClockSkew` jest ustawiony na `TimeSpan.Zero`, więc token wygasły jest odrzucany natychmiast.
