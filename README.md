# Praca Inzynierska

Monorepo aplikacji z jednym wspólnym backendem oraz dwoma klientami:

- `backend` - wspólne API i logika biznesowa w architekturze MVC,
- `frontend/web` - aplikacja webowa,
- `frontend/mobile` - aplikacja mobilna,
- `docs` - dokumentacja projektu.

## Struktura

```text
backend/
├── src/
│   └── Api/
│       ├── Controllers/
│       ├── Models/
│       ├── Views/
│       ├── Data/
│       ├── Services/
│       ├── Repositories/
│       ├── DTOs/
│       ├── Middleware/
│       ├── Configuration/
│       └── Mapping/
└── tests/
    ├── Api.UnitTests/
    │   ├── Controllers/
    │   ├── Services/
    │   └── Repositories/
    └── Api.IntegrationTests/

frontend/
├── web/
│   └── src/
│       ├── components/
│       ├── pages/
│       ├── layouts/
│       ├── services/
│       ├── models/
│       ├── hooks/
│       ├── assets/
│       └── styles/
└── mobile/
    └── src/
        ├── components/
        ├── screens/
        ├── navigation/
        ├── services/
        ├── models/
        ├── hooks/
        ├── assets/
        └── styles/

docs/
```

Backendowe testy jednostkowe i integracyjne są rozdzielone, aby można było
uruchamiać je niezależnie.
