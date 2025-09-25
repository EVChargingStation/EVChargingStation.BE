# EVCharging Station - Backend Microservices

## Tổng quan

Hệ thống backend cho trạm sạc xe điện được xây dựng theo kiến trúc microservices với API Gateway. Hệ thống quản lý toàn bộ quy trình từ xác thực người dùng, quản lý trạm sạc, phiên sạc, thanh toán đến báo cáo.

## Kiến trúc hệ thống

```
EVChargingStation/
├── gateway.ocelot/         # API Gateway - Entry point duy nhất cho client
├── building-blocks/        # Các thành phần dùng chung
│   ├── Shared.Kernel/      # Domain logic, Entity base, Result patterns  
│   └── Shared.Messaging/   # Event messaging, Service contracts
└── services/              # Các microservices
    ├── identity.api/       # Xác thực, phân quyền, quản lý user & vehicle
    ├── station.api/        # Quản lý trạm sạc, connector, vị trí
    ├── session.api/        # Phiên sạc, đặt chỗ, real-time monitoring
    ├── billing.api/        # Thanh toán, hóa đơn, subscription
    └── staff.api/          # Nghiệp vụ nhân viên, báo cáo vận hành
```

## Chi tiết Services

| Service | Chức năng chính |
|---------|----------------|
| **Identity** | JWT authentication, User profiles, Vehicle management |
| **Station** | Station management, Connector status, Location & specifications |
| **Session** | Charging sessions, Real-time monitoring, Reservations (WebSocket) |
| **Billing** | Payment processing, Invoices, Subscription plans |
| **Staff** | Employee operations, Admin reports, Maintenance tracking |

## Yêu cầu & Cài đặt

**Prerequisites:**
- .NET 8.0 SDK
- Docker & Docker Compose  
- SQL Server/PostgreSQL
- Redis

**Quick Start:**
```bash
# Clone & navigate
git clone <repository-url>
cd EVChargingStation.BE

# Run all services
docker-compose up -d

# Development mode (run dependencies first)
docker-compose up -d sqlserver redis
dotnet run --project gateway.ocelot
```

## Kiến trúc kỹ thuật

### Communication Patterns
- **External**: Client → Ocelot Gateway → Services
- **Internal**: HTTP calls (sync) + Message Queue (async)  
- **Real-time**: SignalR for session monitoring

### Database Strategy (Database-per-Service)
- **Identity**: Users, vehicles, authentication data
- **Station**: Stations, connectors, locations
- **Session**: Charging sessions, reservations
- **Billing**: Invoices, payments, subscriptions
- **Staff**: Employee operations, maintenance logs

### Security & Scalability
- **Auth**: JWT tokens, RBAC authorization via Gateway
- **Scale**: Independent service scaling, load balancing, Redis caching
- **Architecture**: Clean Architecture, DDD, CQRS patterns