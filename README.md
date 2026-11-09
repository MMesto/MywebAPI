# MywebAPI

Dit is de backend van mijn portfoliosite. Ik heb hem gebouwd voor opdracht 3 van WPFW (De Haagse Hogeschool).

Op mijn portfoliosite stonden mijn projecten tot nu toe gewoon vast in een JavaScript-array, en mijn blogposts stonden direct in de HTML. Met deze API kun je projecten en blogposts aanmaken, opvragen, wijzigen en verwijderen, en alles wordt opgeslagen in een echte SQL Server-database. Dus ook als je de applicatie opnieuw opstart, is alles er nog.

## Waarmee is het gebouwd?

Ik gebruik **.NET 10** en **ASP.NET Core Web API** met **controllers**. Voor de database gebruik ik **SQL Server Express** en **Entity Framework Core**, zodat ik zelf geen SQL hoef te schrijven. Testen doe ik met **Swagger UI**.

Ik heb voor controllers gekozen omdat ik daar in les 5 al mee begonnen was en de structuur voor mij duidelijk is: elke controller hoort bij één onderwerp (projecten of blogposts).

## Hoe zit de code in elkaar?

Ik heb de code opgedeeld in drie lagen, zodat elk deel zijn eigen taak heeft:

```
MywebAPI
 ├─ Controllers/   → ontvangt de HTTP-requests en kiest de statuscode
 ├─ Services/      → de logica + het omzetten tussen entiteit en DTO
 ├─ Data/          → de DbContext, de entiteiten en de migraties
 ├─ Dtos/          → wat de API ontvangt en terugstuurt
 ├─ appsettings.json
 └─ Program.cs
```

Een request loopt zo door de app: **Controller → Service → DbContext → SQL Server**, en het antwoord gaat als DTO in JSON weer terug.

Een paar afspraken die ik heb aangehouden:
- De controllers praten alleen met de services, nooit direct met de database.
- De API stuurt nooit de database-objecten (entiteiten) zelf naar buiten, maar altijd DTO's. Zo kan de gebruiker bijvoorbeeld ook nooit zelf een `Id` kiezen.
- De omzetting van entiteit naar DTO staat per service op één plek, in de methode `ToDto`.
- De DbContext en de services worden via dependency injection geregistreerd in `Program.cs`.

## Endpoints

Voor projecten en blogposts zijn er elk vijf routes:

| Wat | Projecten | Blogposts |
|---|---|---|
| Alles ophalen | `GET /api/projects` | `GET /api/blogposts` |
| Eén ophalen | `GET /api/projects/{id}` | `GET /api/blogposts/{id}` |
| Toevoegen | `POST /api/projects` | `POST /api/blogposts` |
| Wijzigen | `PUT /api/projects/{id}` | `PUT /api/blogposts/{id}` |
| Verwijderen | `DELETE /api/projects/{id}` | `DELETE /api/blogposts/{id}` |

Bestaat een id niet, dan krijg je een **404** terug. Vul je bijvoorbeeld geen titel in, dan krijg je een **400**, omdat de invoer eerst gecontroleerd wordt.

## Zelf draaien

Je hebt nodig:
- de .NET 10 SDK
- SQL Server Express die lokaal draait (bij mij heet die `localhost\SQLEXPRESS`)
- de EF Core-tool: `dotnet tool install --global dotnet-ef`

### 1. SQL Server instellen

In `appsettings.json` staat waar de database staat:

```json
"ConnectionStrings": {
  "PortfolioDb": "Server=localhost\\SQLEXPRESS;Database=PortfolioDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

Ik log in met mijn Windows-account (`Trusted_Connection=True`), dus er staat geen wachtwoord in de code. Heet jouw SQL Server anders, pas dan het stukje na `Server=` aan.

### 2. Database aanmaken

Ik heb de database gemaakt met een **EF Core-migratie** (`InitialCreate`, in de map `Data/Migrations`). Met dit commando wordt de database `PortfolioDb` met de tabellen `Projects` en `Blogposts` aangemaakt:

```
dotnet ef database update
```

### 3. Starten

```
dotnet run
```

Ga daarna in je browser naar `http://localhost:5229/swagger` (kijk in de terminal bij "Now listening on" als je poort anders is). Daar kun je alle endpoints uitproberen.

## Voorbeeld

Een project toevoegen met `POST /api/projects`:

```json
{
  "titel": "Portfoliosite",
  "beschrijving": "Mijn eigen portfoliosite met projecten en blog",
  "categorie": "Web",
  "gitHubUrl": "https://github.com/MMesto/Portfolio-site",
  "datum": "2026-??-??T00:00:00"
}
```

Een blogpost toevoegen met `POST /api/blogposts`:

```json
{
  "titel": "Mijn eerste blog",
  "inhoud": "Vandaag heb ik een Web API met EF Core en SQL Server gebouwd.",
  "publicatiedatum": "2026-??-??T00:00:00"
}
```

---

*Mohamad Mesto – HBO-ICT Software Engineering, De Haagse Hogeschool*
