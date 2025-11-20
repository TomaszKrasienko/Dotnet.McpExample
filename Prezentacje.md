## Czym jest MCP?

- Stworzony przez firmę Anthropic odpowiedzialną za "Claude"
- Został zaprezentowany w listopadzie 2024 jako odpowiedź na rosnącą potrzebę ustandaryzowania sposobu, w jaki aplikacje AI łączą się z różnymi źródłami danych. 
- Anthropic udostępniło MCP jako open-source
- Komunikacja dwukierunkowa - Umożliwia pełną dwukierunkową komunikację, co oznacza, że asystent AI może zarówno pobierać informacje (np. czytać dokumenty z Google Drive, sprawdzać dane w bazie), jak i wykonywać akcje (np. tworzyć nowe zadania w systemie projektowym, aktualizować rekordy, wysyłać wiadomości).

## Jaki problem rozwiązuje?
- NxM integracji - kazda aplikacja AI wymaga integracji dla kazdego zrodla
- Brak vendor lock-in - zmiana providera AI nie wymaga przepisywania integracji
- Reużywalność - community tworzy gotowe serwery MCP (np. do Postgres, Slack, GitHub)
- Standaryzacja kontekstu - ujednolicony sposób przekazywania danych do LLM (tools, prompts)
