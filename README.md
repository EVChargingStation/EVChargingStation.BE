# EV Charging Station Backend

Hệ thống quản lý trạm sạc xe điện - .NET 8 API

## Tech Stack
- .NET 8.0
- PostgreSQL + Entity Framework Core
- JWT Authentication
- Docker

## Quick Start

```bash
git clone https://github.com/EVChargingStation/EVChargingStation.BE.git
cd EVChargingStation.BE
dotnet ef database update --project EVChargingStation.Domain --startup-project EVChargingStation.API
dotnet run --project EVChargingStation.API
```

Or with Docker:
```bash
docker-compose up -d
```

API: `https://localhost:5001` | Swagger: `https://localhost:5001/swagger`

## MVP Features (2-Person Team)

### Core Features to Implement

#### For EV Drivers
- **Basic Account Management**
  - Registration & login (email only)
  - Simple profile management (personal info, vehicle type)

- **Station Interaction**
  - View charging stations (list view instead of map)
  - View station details (location, status, connector types)
  - Start/stop charging sessions via QR code

- **Payment (Simplified)**
  - Basic payment tracking (kWh-based only)
  - View transaction history
  - Simple electronic receipt (no complex invoicing)

#### For Admin
- **Station Management**
  - Add/edit/remove stations and charging points
  - View station status (online/offline)

- **User Management**
  - Basic user CRUD operations
  - View user list

- **Simple Reporting**
  - Basic revenue report by station
  - Usage frequency report

### Simplified Scope
- **Two User Roles**: 
  - **EV Driver**: End users who charge their electric vehicles
  - **Admin**: System administrators with full access (includes station staff functionality)
- **Features to Exclude**:
  - Map-based station view (use list view instead)
  - Time-based pricing (use kWh pricing only)
  - Payment gateway integration (simulate only)
  - Remote activation/deactivation of charging points
  - Monthly cost reporting
  - Charging habit analytics
  - Business customer accounts (focus on individual users)
- **Basic Entities Only**: User, Station, Connector, Session, Payment

## Development Plan

### Developer 1 - Backend Core
- User authentication & management
- Station & connector CRUD
- Database setup & migrations

### Developer 2 - Business Logic  
- Charging session management
- Payment processing
- API endpoints & documentation

### Timeline: 4-6 weeks
- Week 1-2: Setup, authentication, basic CRUD
- Week 3-4: Charging sessions, payment integration
- Week 5-6: Testing, documentation, deployment