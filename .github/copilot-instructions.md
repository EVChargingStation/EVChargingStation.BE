## Project Overview & Architecture

This project is the backend for an EV Charging Station management system, built with **.NET 8** following a **Clean Architecture** pattern. The goal is to build an MVP (Minimum Viable Product) first.

The solution is divided into four main projects:
- `EVChargingStation.Domain`: The core of the application. Contains all **Entities**, **Enums**, **DTOs**, and the `EvChargingStationDbContext`. This project should have no dependencies on other projects in the solution.
- `EVChargingStation.Application`: Contains the business logic. This includes **service interfaces** (e.g., `IBlobService`), their **implementations** (`BlobService`), and utility classes. It depends only on the `Domain` project.
- `EVChargingStation.Infrastructure`: Implements interfaces defined in the `Application` layer that require external resources. This is where **Repositories** and the `UnitOfWork` pattern are implemented for data access.
- `EVChargingStation.API`: The presentation layer. This is an ASP.NET Core Web API project that exposes endpoints to the outside world. It depends on all other projects and is responsible for Dependency Injection, request/response handling, and authentication.

## Developer Workflow & Commands

**1. Database Migrations:**
The project uses EF Core code-first migrations. When you change an entity in the `Domain` project, you must create and apply a new migration.

- **Create Migration:**
  ```powershell
  dotnet ef migrations add <MigrationName> --project EVChargingStation.Domain --startup-project EVChargingStation.API
  ```
- **Apply Migration (Update Database):**
  ```powershell
  dotnet ef database update --project EVChargingStation.Domain --startup-project EVChargingStation.API
  ```

**2. Running the Application:**
- **Using .NET CLI:**
  ```powershell
  dotnet run --project EVChargingStation.API
  ```
- **Using Docker:**
  ```powershell
  docker-compose up -d
  ```
The API will be available at `https://localhost:5001`, with Swagger UI at `https://localhost:5001/swagger`.

## Key Conventions & Patterns

**1. Focus on the MVP Scope:**
Your primary focus is the MVP defined in `README.md`. This is a 2-person project with a simplified scope.

**MVP Features:**
- **User Management**: Basic registration/login via email, profile management.
- **Station Management**: Basic station info (location, status).
- **Charging Sessions**: Start/stop charging, basic session tracking.
- **Simple Payment**: Fixed pricing and basic payment recording (no real gateways).

**Basic Entities to Implement:**
- `User`
- `Station`
- `Connector`
- `Session`
- `Payment`

**2. Dependency Injection (DI):**
- All services and repositories are registered in `EVChargingStation.API/Architecture/IOCContainer.cs`.
- When adding a new service or repository, register its interface and implementation here.
  ```csharp
  // Example from IOCContainer.cs
  services.AddScoped<IUnitOfWork, UnitOfWork>();
  services.AddScoped<IBlobService, BlobService>();
  ```

**3. Unit of Work Pattern:**
- Data access is managed through the `UnitOfWork` class (`Infrastructure/UnitOfWork.cs`), which provides access to all repositories.
- Inject `IUnitOfWork` into your services instead of injecting individual repositories.
  ```csharp
  // Example in a service constructor
  private readonly IUnitOfWork _unitOfWork;

  public MyService(IUnitOfWork unitOfWork)
  {
      _unitOfWork = unitOfWork;
  }
  ```

**4. API Responses:**
- Use the `ApiResult<T>` class (`Application/Utils/ApiResult.cs`) for consistent API responses.
- For successful responses, use `ApiResult<T>.Success(data)`.
- For errors, use `ApiResult<T>.Fail(error)`.

## Out of Scope (Do Not Implement)
- **Multiple User Roles**: Only the `EV Driver` role is in scope. No `Admin` or `CS Staff` roles.
- **Advanced Features**: 
  - No reservation system.
  - No subscription plans or different pricing models.
  - No AI recommendations or detailed reporting.
  - No real-time station monitoring from the client-side.
- **Advanced Payments**: Do not integrate external payment gateways (e-wallets, banking). Assume a simple, recorded payment model.
- **Social Logins**: Do not implement login/registration via social media accounts.
