Link do repozytorium na Github: https://github.com/MateuszNietupski/Logowanie.git

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

Funkcjonalności 
Autentykacja przy pomocy jwt tokenów oraz refresh tokenów
Responsywna galeria zdjęć z sortowaniem kolejności wyświetlania 
Forum z dynamicznymi kategoriami i podkategoriami postów 
Obsługa wypożyczeń książek przez użytkownika oraz obsługa koszyka na stronie
