# Ścieżki API

## POST /api/auth/login

Loguje istniejącego użytkownika.

Wymagane pola:

```json
{
  "email": "uzytkownik@example.local",
  "password": "SilneHaslo123"
}
```

Odpowiedź zawiera:

- Access Token,
- Refresh Token,
- czas wygaśnięcia Access Tokenu,
- identyfikator i dane użytkownika,
- role użytkownika.

Dostęp: publiczny.

## POST /api/auth/refresh

Rotuje Refresh Token i zwraca nową parę tokenów.

```json
{
  "refreshToken": "<refresh-token>"
}
```

Dostęp: publiczny.

Nieprawidłowy, wygasły lub unieważniony token zwraca `401 Unauthorized`.

## POST /api/auth/revoke

Unieważnia wskazany Refresh Token i kończy możliwość odświeżania tej sesji.

```json
{
  "refreshToken": "<refresh-token>"
}
```

Dostęp: wymaga ważnego Access Tokenu.

Odpowiedzi:

- `200 OK` — token unieważniony,
- `401 Unauthorized` — brak lub nieprawidłowy Access Token,
- `404 Not Found` — wskazany Refresh Token nie istnieje.
