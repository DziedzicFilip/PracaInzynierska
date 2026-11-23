CREATE TABLE role (
    id SERIAL PRIMARY KEY,
    nazwa VARCHAR(50) NOT NULL
);

CREATE TABLE uzytkownicy (
    id SERIAL PRIMARY KEY,
    rola_id INT REFERENCES role(id),
    nazwa_uzytkownika VARCHAR(100) NOT NULL UNIQUE,
    haslo VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    numer_telefonu VARCHAR(20)
);

CREATE TABLE sale (
    id SERIAL PRIMARY KEY,
    nazwa VARCHAR(100) NOT NULL,
    typ_sali VARCHAR(50) NOT NULL DEFAULT 'dydaktyczna'
);

CREATE TABLE alergeny (
    id SERIAL PRIMARY KEY,
    nazwa VARCHAR(100) NOT NULL
);

CREATE TABLE diety (
    id SERIAL PRIMARY KEY,
    nazwa VARCHAR(100) NOT NULL
);

CREATE TABLE zajecia (
    id SERIAL PRIMARY KEY,
    nazwa VARCHAR(150) NOT NULL,
    typ_zajec VARCHAR(50) NOT NULL DEFAULT 'podstawowe'
);

CREATE TABLE posilki (
    id SERIAL PRIMARY KEY,
    data DATE NOT NULL,
    nazwa VARCHAR(150) NOT NULL,
    opis TEXT
);

CREATE TABLE pomoce_dydaktyczne (
    id SERIAL PRIMARY KEY,
    nazwa VARCHAR(200) NOT NULL,
    typ VARCHAR(100),
    stan_ilosciowy INT NOT NULL DEFAULT 1,
    sala_przypisana_id INT REFERENCES sale(id)
);

CREATE TABLE posilek_alergen (
    posilek_id INT NOT NULL REFERENCES posilki(id),
    alergen_id INT NOT NULL REFERENCES alergeny(id),
    PRIMARY KEY (posilek_id, alergen_id)
);

CREATE TABLE pracownicy (
    id SERIAL PRIMARY KEY,
    uzytkownik_id INT NOT NULL UNIQUE REFERENCES uzytkownicy(id),
    imie VARCHAR(100) NOT NULL,
    nazwisko VARCHAR(100) NOT NULL
);

CREATE TABLE rodzice (
    id SERIAL PRIMARY KEY,
    uzytkownik_id INT NOT NULL UNIQUE REFERENCES uzytkownicy(id),
    imie VARCHAR(100) NOT NULL,
    nazwisko VARCHAR(100) NOT NULL,
    kod_pin_odbior VARCHAR(10)
);

CREATE TABLE grupy (
    id SERIAL PRIMARY KEY,
    nazwa VARCHAR(100) NOT NULL,
    wiek_dzieci VARCHAR(50),
    wychowawca_id INT REFERENCES pracownicy(id),
    sala_id INT REFERENCES sale(id)
);

CREATE TABLE wydarzenia_kalendarz (
    id SERIAL PRIMARY KEY,
    nazwa VARCHAR(255) NOT NULL,
    opis TEXT,
    data_wydarzenia DATE NOT NULL,
    godzina_rozpoczecia TIME,
    godzina_zakonczenia TIME,
    pracownik_odpowiedzialny_id INT REFERENCES pracownicy(id)
);

CREATE TABLE grafik_pracy (
    id SERIAL PRIMARY KEY,
    pracownik_id INT NOT NULL REFERENCES pracownicy(id),
    data DATE NOT NULL,
    godzina_rozpoczecia TIME NOT NULL,
    godzina_zakonczenia TIME NOT NULL
);

CREATE TABLE nieobecnosci_pracownikow (
    id SERIAL PRIMARY KEY,
    pracownik_id INT NOT NULL REFERENCES pracownicy(id),
    zastepca_id INT REFERENCES pracownicy(id),
    powod VARCHAR(100),
    data_od DATE NOT NULL,
    data_do DATE NOT NULL
);

CREATE TABLE nadgodziny_pracownikow (
    id SERIAL PRIMARY KEY,
    pracownik_id INT NOT NULL REFERENCES pracownicy(id),
    data DATE NOT NULL,
    ilosc_godzin DECIMAL(5,2) NOT NULL,
    powod VARCHAR(255)
);

CREATE TABLE badania_szkolenia_pracownikow (
    id SERIAL PRIMARY KEY,
    pracownik_id INT NOT NULL REFERENCES pracownicy(id),
    typ VARCHAR(100) NOT NULL,
    data_waznosci DATE NOT NULL
);

CREATE TABLE rezerwacje_sal (
    id SERIAL PRIMARY KEY,
    sala_id INT NOT NULL REFERENCES sale(id),
    pracownik_id INT NOT NULL REFERENCES pracownicy(id),
    cel VARCHAR(255),
    data_rezerwacji DATE NOT NULL,
    godzina_od TIME NOT NULL,
    godzina_do TIME NOT NULL
);

CREATE TABLE rezerwacje_pomocy_dydaktycznych (
    id SERIAL PRIMARY KEY,
    pomoc_id INT NOT NULL REFERENCES pomoce_dydaktyczne(id),
    pracownik_id INT NOT NULL REFERENCES pracownicy(id),
    data_od TIMESTAMP NOT NULL,
    data_do TIMESTAMP NOT NULL
);

CREATE TABLE usterki_pomocy_dydaktycznych (
    id SERIAL PRIMARY KEY,
    pomoc_id INT NOT NULL REFERENCES pomoce_dydaktyczne(id),
    pracownik_zglaszajacy_id INT NOT NULL REFERENCES pracownicy(id),
    opis TEXT NOT NULL,
    data_zgloszenia TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50) NOT NULL DEFAULT 'Zgłoszona'
);

CREATE TABLE dzieci (
    id SERIAL PRIMARY KEY,
    imie VARCHAR(100) NOT NULL,
    nazwisko VARCHAR(100) NOT NULL,
    data_urodzenia DATE NOT NULL,
    grupa_id INT REFERENCES grupy(id)
);

CREATE TABLE plany_zajec (
    id SERIAL PRIMARY KEY,
    zajecie_id INT NOT NULL REFERENCES zajecia(id),
    grupa_id INT NOT NULL REFERENCES grupy(id),
    sala_id INT REFERENCES sale(id),
    pracownik_id INT REFERENCES pracownicy(id),
    dzien_tygodnia INT NOT NULL,
    czas_rozpoczecia TIME NOT NULL,
    czas_zakonczenia TIME NOT NULL
);

CREATE TABLE dziennik_zajec (
    id SERIAL PRIMARY KEY,
    grupa_id INT NOT NULL REFERENCES grupy(id),
    pracownik_id INT NOT NULL REFERENCES pracownicy(id),
    data DATE NOT NULL,
    temat VARCHAR(255) NOT NULL,
    podstawa_programowa TEXT
);

CREATE TABLE plan_dnia_grupy (
    id SERIAL PRIMARY KEY,
    grupa_id INT NOT NULL REFERENCES grupy(id),
    godzina_od TIME NOT NULL,
    godzina_do TIME NOT NULL,
    czynnosc VARCHAR(255) NOT NULL
);

CREATE TABLE ogloszenia (
    id SERIAL PRIMARY KEY,
    autor_id INT NOT NULL REFERENCES pracownicy(id),
    grupa_id INT REFERENCES grupy(id),
    tytul VARCHAR(255) NOT NULL,
    tresc TEXT NOT NULL,
    data_publikacji TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE rodzic_dziecko (
    rodzic_id INT NOT NULL REFERENCES rodzice(id),
    dziecko_id INT NOT NULL REFERENCES dzieci(id),
    PRIMARY KEY (rodzic_id, dziecko_id)
);

CREATE TABLE osoby_upowaznione_odbior (
    id SERIAL PRIMARY KEY,
    dziecko_id INT NOT NULL REFERENCES dzieci(id),
    imie VARCHAR(100) NOT NULL,
    nazwisko VARCHAR(100) NOT NULL,
    jednorazowy_kod_pin VARCHAR(10)
);

CREATE TABLE dziecko_alergen (
    dziecko_id INT NOT NULL REFERENCES dzieci(id),
    alergen_id INT NOT NULL REFERENCES alergeny(id),
    PRIMARY KEY (dziecko_id, alergen_id)
);

CREATE TABLE dziecko_dieta (
    dziecko_id INT NOT NULL REFERENCES dzieci(id),
    dieta_id INT NOT NULL REFERENCES diety(id),
    PRIMARY KEY (dziecko_id, dieta_id)
);

CREATE TABLE odwolania_posilkow (
    id SERIAL PRIMARY KEY,
    dziecko_id INT NOT NULL REFERENCES dzieci(id),
    data_posilku DATE NOT NULL,
    data_zgloszenia TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE incydenty (
    id SERIAL PRIMARY KEY,
    dziecko_id INT NOT NULL REFERENCES dzieci(id),
    rodzic_id INT REFERENCES rodzice(id),
    pracownik_id INT NOT NULL REFERENCES pracownicy(id),
    powod TEXT NOT NULL,
    typ_incydentu VARCHAR(100) NOT NULL DEFAULT 'zdrowotny',
    data_zgloszenia TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    protokol_bhp BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE przeciwwskazania_medyczne (
    id SERIAL PRIMARY KEY,
    dziecko_id INT NOT NULL REFERENCES dzieci(id),
    opis TEXT NOT NULL
);

CREATE TABLE leki_dzieci (
    id SERIAL PRIMARY KEY,
    dziecko_id INT NOT NULL REFERENCES dzieci(id),
    nazwa_leku VARCHAR(150) NOT NULL,
    dawka VARCHAR(100) NOT NULL,
    godzina_podania TIME,
    data_od DATE NOT NULL,
    data_do DATE NOT NULL,
    uwagi TEXT,
    zgoda_rodzica BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE karty_obserwacji_pedagogicznej (
    id SERIAL PRIMARY KEY,
    dziecko_id INT NOT NULL REFERENCES dzieci(id),
    pracownik_id INT NOT NULL REFERENCES pracownicy(id),
    specjalizacja VARCHAR(100) NOT NULL,
    notatka TEXT NOT NULL,
    data_wpisu TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE dziecko_plan_zajec (
    dziecko_id INT NOT NULL REFERENCES dzieci(id),
    plan_zajec_id INT NOT NULL REFERENCES plany_zajec(id),
    PRIMARY KEY (dziecko_id, plan_zajec_id)
);

CREATE TABLE obecnosci (
    id SERIAL PRIMARY KEY,
    dziecko_id INT NOT NULL REFERENCES dzieci(id),
    data DATE NOT NULL,
    status VARCHAR(50) NOT NULL,
    godzina_przyjscia TIME,
    godzina_odbioru TIME,
    rodzic_odbierajacy_id INT REFERENCES rodzice(id),
    osoba_upowazniona_id INT REFERENCES osoby_upowaznione_odbior(id),
    metoda_rejestracji VARCHAR(50)
);

CREATE TABLE rejestr_podania_lekow (
    id SERIAL PRIMARY KEY,
    lek_id INT NOT NULL REFERENCES leki_dzieci(id),
    pracownik_id INT NOT NULL REFERENCES pracownicy(id),
    data_podania TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    potwierdzenie_podpisu BOOLEAN NOT NULL DEFAULT TRUE
);
