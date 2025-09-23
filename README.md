# EV Charging Station - Backend Microservices

## 🏗️ Architecture Overview

This project follows a microservices architecture pattern with API Gateway and shared building blocks. The system is designed to handle electric vehicle charging station operations including user management, station management, charging sessions, billing, and reporting.

## 📁 Project Structure

```
EVChargingStation/
├── EVChargingStation.sln
├── docker-compose.yml
├── docker-compose.override.yml
├── gateway.ocelot/
│   ├── Program.cs
│   ├── ocelot.json
│   └── gateway.ocelot.csproj
├── building-blocks/
│   ├── Shared.Kernel/
│   │   ├── Entity.cs
│   │   ├── ValueObject.cs
│   │   ├── Result.cs
│   │   └── Shared.Kernel.csproj
│   └── Shared.Messaging/
│       ├── Events/
│       │   ├── ChargingSessionStarted.cs
│       │   ├── ChargingSessionCompleted.cs
│       │   ├── InvoiceCreated.cs
│       │   └── ...
│       └── Shared.Messaging.csproj
└── services/
    ├── identity.api/
    │   ├── Controllers/
    │   │   ├── AuthController.cs
    │   │   ├── UserController.cs
    │   │   └── VehicleController.cs
    │   ├── Data/
    │   │   └── IdentityDbContext.cs
    │   ├── Models/
    │   │   ├── User.cs
    │   │   └── Vehicle.cs
    │   └── identity.api.csproj
    ├── station.api/
    │   ├── Controllers/
    │   │   ├── StationController.cs
    │   │   ├── ConnectorController.cs
    │   │   └── AdminStationController.cs
    │   ├── Data/
    │   │   └── StationDbContext.cs
    │   └── station.api.csproj
    ├── session.api/
    │   ├── Controllers/
    │   │   ├── ReservationController.cs
    │   │   └── SessionController.cs
    │   ├── Hubs/
    │   │   └── SessionsHub.cs
    │   ├── Data/
    │   │   └── SessionDbContext.cs
    │   └── session.api.csproj
    ├── billing.api/
    │   ├── Controllers/
    │   │   ├── InvoiceController.cs
    │   │   ├── PaymentController.cs
    │   │   ├── PlanController.cs
    │   │   └── UserPlanController.cs
    │   ├── Data/
    │   │   └── BillingDbContext.cs
    │   └── billing.api.csproj
    ├── staff.api/
    │   ├── Controllers/
    │   │   ├── StaffSessionController.cs
    │   │   └── ReportController.cs
    │   ├── Data/
    │   │   └── StaffDbContext.cs
    │   └── staff.api.csproj
    └── reporting.api/
        ├── Controllers/
        │   ├── ReportController.cs
        │   └── RecommendationController.cs
        ├── Data/
        │   └── ReportingDbContext.cs
        └── reporting.api.csproj
```

## 🔧 Components

### API Gateway
- **gateway.ocelot**: Entry point for all client requests, handles routing, authentication, and load balancing using Ocelot

### Building Blocks
- **Shared.Kernel**: Common domain building blocks (Entity, ValueObject, Result patterns)
- **Shared.Messaging**: Domain events and messaging contracts for inter-service communication

### Microservices

#### 1. Identity Service (`identity.api`)
**Responsibilities:**
- User authentication and authorization
- User profile management
- Vehicle registration and management
- JWT token generation and validation

**Key Features:**
- User registration/login
- Vehicle management per user
- Role-based access control

#### 2. Station Service (`station.api`)
**Responsibilities:**
- Charging station management
- Connector availability tracking
- Station location and specifications
- Administrative station operations

**Key Features:**
- Station CRUD operations
- Connector status management
- Station search and filtering

#### 3. Session Service (`session.api`)
**Responsibilities:**
- Charging session lifecycle management
- Real-time session monitoring
- Reservation system
- WebSocket connections for live updates

**Key Features:**
- Session start/stop operations
- Real-time charging status via SignalR
- Reservation management

#### 4. Billing Service (`billing.api`)
**Responsibilities:**
- Invoice generation and management
- Payment processing
- Subscription plans
- User billing history

**Key Features:**
- Dynamic pricing calculations
- Multiple payment methods support
- Subscription plan management

#### 5. Staff Service (`staff.api`)
**Responsibilities:**
- Staff operations and monitoring
- Administrative session management
- Operational reporting for staff

**Key Features:**
- Staff dashboard functionality
- Session monitoring for operators
- Maintenance scheduling

#### 6. Reporting Service (`reporting.api`)
**Responsibilities:**
- Business intelligence and analytics
- Usage statistics and insights
- Recommendation engine
- Data aggregation from other services

**Key Features:**
- Usage analytics and trends
- Performance reports
- AI-powered recommendations

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- Docker & Docker Compose
- SQL Server (or PostgreSQL)
- Redis (for caching and messaging)

### Running the Application

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd EVChargingStation.BE
   ```

2. **Run with Docker Compose**
   ```bash
   docker-compose up -d
   ```

3. **Run individual services (Development)**
   ```bash
   # Start dependencies first
   docker-compose up -d sqlserver redis
   
   # Run individual services
   dotnet run --project services/identity.api
   dotnet run --project services/station.api
   # ... other services
   
   # Run API Gateway
   dotnet run --project gateway.ocelot
   ```

## 🔗 Service Communication

- **API Gateway**: All external requests go through Ocelot gateway
- **Inter-service Communication**: HTTP calls for synchronous operations
- **Event-driven Communication**: Message queues for asynchronous operations
- **Real-time Updates**: SignalR hubs for live session monitoring

## 📊 Database Strategy

Each service maintains its own database following the database-per-service pattern:
- **Identity DB**: User accounts, vehicles, authentication data
- **Station DB**: Station information, connectors, locations
- **Session DB**: Active sessions, reservations, session history
- **Billing DB**: Invoices, payments, subscription plans
- **Staff DB**: Staff operations, maintenance records
- **Reporting DB**: Aggregated data, analytics, reports

## 🔐 Security

- JWT-based authentication
- API Gateway handles authentication/authorization
- Service-to-service communication secured
- Role-based access control (RBAC)

## 📈 Scalability Features

- Independent service scaling
- Load balancing via API Gateway
- Database sharding capabilities
- Caching strategies with Redis
- Event-driven architecture for loose coupling

## 🛠️ Development

Each service is independently deployable and follows:
- Clean Architecture principles
- CQRS pattern where applicable
- Domain-driven design
- Comprehensive unit and integration testing