# Refresh Token i wylogowanie

## Refresh Token

Refresh Token jest kryptograficznie losową wartością zapisywaną w tabeli `RefreshTokens`.

Przy logowaniu lub odświeżaniu serwis tworzy nową wartość i zapisuje:

- użytkownika,
- datę utworzenia,
- datę wygaśnięcia,
- stan unieważnienia.

## Odświeżanie sesji

`POST /api/auth/refresh` przyjmuje aktualny Refresh Token.

Serwis:

1. wyszukuje token w bazie,
2. sprawdza, czy nie wygasł,
3. sprawdza, czy nie został unieważniony,
4. oznacza stary token jako unieważniony,
5. generuje nowy Access Token,
6. generuje i zapisuje nowy Refresh Token.

Mechanizm ten nazywa się rotacją tokenów.

## Ponowne użycie unieważnionego tokenu

Jeżeli unieważniony token zostanie użyty ponownie, serwis unieważnia wszystkie aktywne Refresh Tokeny tego użytkownika.

Takie działanie ogranicza skutki kradzieży tokenu i wymusza ponowne zalogowanie się po wykryciu ponownego użycia.

## Revoke

`POST /api/auth/revoke` jest chronionym endpointem wylogowania. Przyjmuje Refresh Token i oznacza go jako unieważniony.

Revoke nie usuwa Access Tokenu natychmiast z pamięci klienta ani nie dodaje go do listy odwołanych tokenów. Access Token pozostaje ważny do swojej daty wygaśnięcia. Revoke jest przeznaczony przede wszystkim do zakończenia sesji i uniemożliwienia dalszego odświeżania.
