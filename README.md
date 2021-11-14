# Project Details

Funkcjonalności:
- Rejestracja i Logowanie
- Rejestracja przez social media
- Rejestracja potwierdzana mailem
- Resetowanie hasła
- Publiczny profil
- Statystyki profilu (ilosć odwieczonych miast, państw itp.)
- Lista znajomych z zapraszaniem 
- Obejrzenie profilu znajomego
- Interaktywna mapa
- Znaczniki miejsc odwiedzonych
- Lista miejsc odwiedzionych
- Popup po kliknięciu na marker z informacjami
- Możliwość wpisania opisu do wybranego dodanego miejsca
- Dark mode i light mode
- Dwujęczność (EN/PL)
- Tagi ???
- Możliwosc wyszukania lokalizacji

Technologie:
- .NET
- C#
- Docker
- HTML
- CSS
- JAVASCRIPT
- REACT

## Uruchomienie projektu lokalnie

* Pobierz repozytorium
* Zainstaluj SQL Server Management Studio
* Za pomocą Visual Studio Installer pobierz dodatek

![image](https://user-images.githubusercontent.com/72551592/141694864-57c2c209-d3eb-4565-a1a6-bbed10c14d75.png)
* Otwórz ```solution``` za pomocą Visual Studio
* Skonfiguruj odpowiednio wszystkie pozycje w pliku ```appsetting.json``` w projekcie WebApi
* Ustaw projekt ```WebApi``` jako startowy w celu uruchomienia
(przed tym wpisz komendę ```Update-Database``` w konsoli mając ustawiony projekt domyślny ```Storage```)

![image](https://user-images.githubusercontent.com/72551592/141695597-69685c10-c0e6-4d76-93da-5828ddd59426.png)

lub ```docker-compose``` aby uruchomić aplikację w kontenerze
(w tym wypadku należy również mieć zainstalowanego ```Docker Desktop```)
* Uruchom aplikację potwierdzając certyfikat bezpieczeństwa
* Aplikacja otworzy domyślną stronę swagerra

Autorzy:
- Mariusz Skuza (Lider/PM/DevOps/Backend)
- Michał Nocuń (Tester/Backend)
- Piotr Tekieli (Backend)
- Kacper Frankowski (Frontend)
