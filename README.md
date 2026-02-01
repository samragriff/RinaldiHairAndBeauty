# Rinaldi Hair and Beauty

Demo full-stack app: .NET 10 Web API + SQL Server (Docker) + (React UI to be added step by step).

## What’s in place

- **Rinaldis.API** – Minimal API with Endpoints (no controllers): Swagger, CORS for Vite, MediatR, Enquiry endpoints.
- **Rinaldis.Core** – Domain entities, CQRS (CreateEnquiry, GetEnquiries), IAppDbContext (handlers use context directly).
- **Rinaldis.Infrastructure** – EF Core, SQL Server, `AppDbContext`, Enquiry configuration, DI.
- **Rinaldis.Shared** – DTOs, enums (`EnquiryStatus`).

First vertical slice: **Contact/Enquiry** form that posts to the API and saves to the DB. Auth can come later.

## How to run (API + DB)

### 1. SQL Server in Docker

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<PASSWORD>" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

Replace `<PASSWORD>` with a strong password and set the same value in **Rinaldis.API/appsettings.Development.json** under `ConnectionStrings:Default`.

### 2. EF migrations (first time)

Install the EF Core global tool if you don’t have it:

```bash
dotnet tool install --global dotnet-ef
```

(If it’s already installed but outdated: `dotnet tool update --global dotnet-ef`.)

If `dotnet ef` is not found even when installed, the tools folder may not be on your PATH. On Windows it’s usually `%USERPROFILE%\.dotnet\tools`. Add it to PATH, or open a **new** terminal (so it picks up PATH), or run: `dotnet tool run dotnet-ef -- migrations add Initial ...` from the repo root (same for `database update`).

- **Default project:** `Rinaldis.Infrastructure`
- **Startup project:** `Rinaldis.API`

From repo root:

```bash
dotnet ef migrations add Initial --project Rinaldis.Infrastructure --startup-project Rinaldis.API --output-dir Data/Migrations
dotnet ef database update --project Rinaldis.Infrastructure --startup-project Rinaldis.API
```

### 3. Run the API

Open the solution in Visual Studio, set **Rinaldis.API** as startup project, and run (F5), or:

```bash
cd Rinaldis.API
dotnet run
```

- API: e.g. `https://localhost:7xxx` (see launchSettings.json).
- Swagger: `https://localhost:7xxx/swagger`.

### API endpoints

- **POST** `/api/enquiries` – body: `CreateEnquiryRequest` (Name, Email, Phone?, Message, PreferredDateUtc?) → 201 + `EnquiryResponse`.
- **GET** `/api/enquiries` – returns latest 50 enquiries.

---

**UI:** A React (Vite) front end will be added step by step so you can follow along. Future: auth/admin page.
