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

### Phase 1 - Core Functionality
- **User Management**: Basic registration/login, profile management
- **Station Management**: Basic station info, location, status
- **Charging Sessions**: Start/stop charging, basic session tracking
- **Simple Payment**: Fixed pricing, basic payment recording

### Simplified Scope
- **Single Role**: EV Driver only (no staff/admin roles)
- **No Advanced Features**: 
  - No reservations system
  - No subscription plans  
  - No AI recommendations
  - No detailed reporting
  - No real-time monitoring
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