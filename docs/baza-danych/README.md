# Schemat Bazy Danych – System Zarządzania Placówką Przedszkolną

Katalog zawiera pełną specyfikację bazy danych projektu inżynierskiego (37 tabel domenowych i słownikowych wraz z relacjami):

- [`diagram_bazy.pdf`](./diagram_bazy.pdf) – wizualny diagram ERD wygenerowany w `dbdiagram.io`,
- [`schemat_bazy.dbml`](./schemat_bazy.dbml) – źródłowa definicja schematu w formacie DBML (`dbdiagram.io`),
- [`schemat_bazy.sql`](./schemat_bazy.sql) – pełny skrypt SQL (`CREATE TABLE` wraz z kluczami obcymi i złożonymi),
- Implementacja **Entity Framework Core (Code-First dla MS SQL Server)**:
  - Kontekst bazy danych: [`backend/Backend/Api/Data/ApplicationDbContext.cs`](../../backend/Backend/Api/Data/ApplicationDbContext.cs)
  - Modele encji: [`backend/Backend/Api/Models/`](../../backend/Backend/Api/Models/)
  - Migracje: [`backend/Backend/Api/Migrations/`](../../backend/Backend/Api/Migrations/)

## Zestawienie modułów i tabel (37 tabel)

1. **Uwierzytelnianie i użytkownicy:** `role` (`AspNetRoles`), `uzytkownicy` (`AspNetUsers`), `pracownicy`, `rodzice`, `RefreshTokens`
2. **Struktura placówki i sale:** `sale`, `grupy`, `pomoce_dydaktyczne`, `rezerwacje_sal`, `rezerwacje_pomocy_dydaktycznych`, `usterki_pomocy_dydaktycznych`
3. **Kadry i czas pracy:** `grafik_pracy`, `nieobecnosci_pracownikow`, `nadgodziny_pracownikow`, `badania_szkolenia_pracownikow`
4. **Dzieci, rodzice i odbiory:** `dzieci`, `rodzic_dziecko`, `osoby_upowaznione_odbior`, `obecnosci`
5. **Dydaktyka i komunikacja:** `zajecia`, `plany_zajec`, `dziecko_plan_zajec`, `dziennik_zajec`, `plan_dnia_grupy`, `wydarzenia_kalendarz`, `ogloszenia`, `karty_obserwacji_pedagogicznej`
6. **Stołówka, diety i alergeny:** `alergeny`, `diety`, `posilki`, `posilek_alergen`, `dziecko_alergen`, `dziecko_dieta`, `odwolania_posilkow`
7. **Zdrowie, leki i bezpieczeństwo (BHP):** `leki_dzieci`, `rejestr_podania_lekow`, `przeciwwskazania_medyczne`, `incydenty`
