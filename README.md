# Library-Forum

# Funkcjonalności 

- Logowanie i rejestracja użytkowników z wykorzystaniem JWT tokenów. Dodatkowo zaimplementowana została obsługa refresh tokenów, a także flagowanie dostępu względem roli użytkownika.
- Responsywna galeria zdjęć. 
Forum z dynamicznymi kategoriami i podkategoriami postów oraz obsługą komentarzy. 
Obsługa wypożyczeń książek przez użytkownika oraz obsługa koszyka na stronie.

# Użyte technologie
- .Net 8.0
- React.js
- MUI
- PostgreSQL
- Entity Framework
- AutoMapper
- JWT

# Backend

# Zastosowane wzorce projektowe

• **Dependency Injection** - serwisy i repozytoria są rejestrowane w kontenerze DI i wstrzykiwane gdy są potrzebne. Dzięki temu kod nie jest ze sobą sztywno powiązany i łatwiej jest go testować i ewentualnie zmieniać konkretne fragmenty implementacji.
• **Layered Architecture**:
 - Repositories (Data Access Layer) - komunikacja z bazą danych
 - Services (Business Logic Layer) - logika biznesowa
 - Controllers (Presentation Layer) - obsługa żądań HTTP
Taki podział zapewnia modularności i separację odpowiedzialności.
 
• **Mapper Pattern** - wykorzystany do konwersji przy użyciu AutoMappera danych pomiędzy obiektami domenowymi a DTO. Dzięki temu API jest odseparowane od modeli bazodanowych i minimalizuje ryzyko wycieku encji.

# Struktura projektu

• API:
 - wwwroot/GalleryImages - katalog do przechowywania plików statycznych(zapisanych obrazów).
 - Controllers - kontrolery obsługujące żądania HTTP i komunikujące się z warstwą serwisową.
 - MappersProfiles - profile mapowania AutoMappera pomiędzy encjami domenowymi a DTO.
 - Services - serwisy zawierające logikę biznesową aplikacji.
   
• DataService:
 - Data - zawiera AppDbContext, zawierajacy zbiory encji odpowiadające tabelom bazodanowym, a także konfiguracje relacji między nimi.
 - Repositories - repozytoria obsługujące zapytania do bazy danych.
   
• Entities:
 - DTOs - modele przesyłane przez API do frontendu.
 - Models - encje bazodanowe odwzorowujące tabele bazodanowe.


Uruchomienie backendu
Wymaga .net SDK 8.0

Baza danych
Baza danych wymaga stworzenia lokalnego serwera bazodanowego PostgreSQL np poprzez narzędzie pgAdmin
W projekcie connectionString łączący z bazą daną posiada następujące wartości
User ID=postgres;Password=admin;Server=localhost;Port=5432;Database=ServerDb;

Kiedy stworzona zostanie baza wystaczy w terminalu na poziomie .\Backend\Projekt> stworzyć migracje do bazy danych poprzez "dotnet ef migrations add "init" "
a następnie przy pomocy "dotnet ef database update" stworzyć strukture bazy danych potrzebna do działania backendu


Uruchomieniu frontendu 
Wymagana instalacja Node js
Przejście do głównego folderu frontendu i wykonanie poleceń:
Do instalacji dependencji: `npm install`
Do uruchomienia: `npm start` 
