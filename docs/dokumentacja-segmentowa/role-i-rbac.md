# Role i autoryzacja RBAC

## Dostęp oparte na rolach

Kontrolery i ich metody mogą używać atrybutu `[Authorize]`.

Przykład wymagający konkretnej roli:

```csharp
[Authorize(Roles = SystemRoles.Administrator)]
```

Przykład endpointu dostępnego po zalogowaniu dla dowolnej roli:

```csharp
[Authorize]
```

## Role systemowe

System definiuje role:

- `SuperAdministrator`,
- `Administrator`,
- `Wychowawca`,
- `Rodzic`.

`SuperAdministrator` i `Administrator` są tworzeni jako dwa osobne claimy roli. Konto startowe SuperAdministratora powinno mieć obie role.

## Kody odpowiedzi

- brak tokenu — `401 Unauthorized`,
- token ważny, ale bez wymaganej roli — `403 Forbidden`,
- token ważny i wymagania spełnione — normalna odpowiedź endpointu.
