# Przykładowy screenshot

![obraz](https://github.com/user-attachments/assets/c9d0db26-cab9-4b4a-911c-f93dad253110)

# Instrukcja Użycia Aplikacji z Docker i Docker Compose

## **Wymagania: Docker + Docker Compose**

---

## **1. Rozpakowanie i Przygotowanie Plików**

1. Wypakuj pobrany plik ZIP.  
2. Otwórz terminal (MacOS/Linux) lub PowerShell (Windows) i przejdź do folderu:  
   ```bash
   cd ~/Downloads/JhonnyEventCalendar
   ```

---

## **2. Edycja Ścieżek w `docker-compose.yml`**

1. Otwórz plik `docker-compose.yml` w edytorze tekstu lub IDE.  
2. Zmień ścieżki do pliku z terminami, jeżeli potrzebujesz (lub zostaw domyślne ustawienia).  
   Domyślne ustawienia:  
   ```yaml
   environment:
     IMAGE_PATH: "/app/input/att.0VdYasWgp7xb5IauEo-WsStaxNZba1SaCi_u8PcnZM8.jpg"
     ICS_PATH: "/app/output/treningi_calendar.ics"
   ```

3. (Opcjonalnie) Skopiuj swój obrazek do folderu `./input`, jeśli zostawiasz domyślne ścieżki:  
   ```bash
   cp /ścieżka/do/obrazka.jpg ./input/
   ```

---

## **3. Instalacja Dockera i Docker Compose**

### **MacOS**

1. Zainstaluj Dockera:
   ```bash
   brew install --cask docker
   ```
2. Zainstaluj Docker Compose:
   ```bash
   brew install docker-compose
   ```
3. Uruchom Docker Desktop:
   ```bash
   open /Applications/Docker.app
   ```
4. Zweryfikuj wersje:
   ```bash
   docker --version
   docker compose version
   ```

### **Windows**

1. Pobierz i zainstaluj [Docker Desktop](https://www.docker.com/products/docker-desktop/).  
2. Otwórz PowerShell jako administrator.  
3. Zainstaluj WSL 2 (jeśli nie jest zainstalowane):  
   ```powershell
   wsl --install
   wsl --set-default-version 2
   ```
4. Zweryfikuj wersje Dockera i Compose:  
   ```bash
   docker --version
   docker compose version
   ```

---

## **4. Uruchomienie Aplikacji**

1. Przejdź do folderu projektu:
   ```bash
   cd ~/Downloads/JhonnyEventCalendar
   ```

2. Uruchom aplikację:
   ```bash
   docker compose up
   ```

3. **Wynik**:  
   Plik `.ics` zostanie wygenerowany w folderze `./output`.

4. **Zatrzymanie kontenera (opcjonalnie):**
   ```bash
   docker compose down
   ```
   > **Uwaga:** Polecenie to usuwa kontener z Dockera, zwalniając miejsce na dysku.

---

## **5. Debugowanie**

1. **Sprawdź logi kontenera:**  
   ```bash
   docker logs <container-id>
   ```

2. **Wejdź do kontenera:**  
   ```bash
   docker exec -it <container-id> /bin/bash
   ```

3. **Wyczyść stare obrazy i kontenery:**  
   ```bash
   docker system prune -af
   ```

---

## **6. Aktualizacja i Przebudowa Obrazu (opcjonalnie)**

1. Zaktualizuj kod lub Dockerfile.  
2. Przebuduj obraz:
   ```bash
   docker compose build
   ```
3. Uruchom aplikację ponownie:
   ```bash
   docker compose up
   ```

---

To wszystko! 😊 Jeśli pojawią się jakieś pytania, śmiało pytaj!
