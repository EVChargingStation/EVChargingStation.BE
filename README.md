# Trạm Sạc Xe Điện - Backend Microservices

## Tổng quan kiến trúc

Project này được xây dựng theo kiến trúc microservices, sử dụng API Gateway và các khối xây dựng (building blocks) dùng chung. Hệ thống được thiết kế để xử lý các hoạt động của trạm sạc xe điện, bao gồm quản lý người dùng, quản lý trạm, các phiên sạc, thanh toán và báo cáo.

## Cấu trúc Project

```
EVChargingStation/
├── EVChargingStation.sln
├── docker-compose.yml
├── docker-compose.override.yml
├── gateway.ocelot/         # API Gateway, là cổng vào duy nhất cho tất cả request từ client.
├── building-blocks/
│   ├── Shared.Kernel/      # Chứa các logic domain và class cơ sở dùng chung cho toàn bộ hệ thống.
│   └── Shared.Messaging/   # Hỗ trợ giao tiếp giữa các services, thường qua message queue.
└── services/
    ├── identity.api/       # Quản lý định danh, xử lý xác thực, phân quyền và thông tin người dùng.
    ├── station.api/        # Quản lý thông tin các trạm sạc, trạng thái và các cổng sạc (connector).
    ├── session.api/        # Quản lý các phiên sạc (charging session) và việc đặt chỗ trước.
    ├── billing.api/        # Xử lý các vấn đề về thanh toán, hóa đơn và các gói dịch vụ.
    ├── staff.api/          # Cung cấp chức năng cho nhân viên, ví dụ như báo cáo, bảo trì.
    └── reporting.api/      # Tổng hợp, phân tích dữ liệu và đưa ra các báo cáo, đề xuất.
```

## Các thành phần

### API Gateway
- **gateway.ocelot**: Điểm vào (entry point) cho tất cả request từ client. Chịu trách nhiệm routing, xác thực và cân bằng tải (load balancing) bằng Ocelot.

### Building Blocks (Khối xây dựng)
- **Shared.Kernel**: Chứa các thành phần domain chung như (Entity, ValueObject, Result patterns).
- **Shared.Messaging**: Chứa các domain event và hợp đồng (contract) để giao tiếp giữa các service.

### Microservices

#### 1. Identity Service (`identity.api`)
**Trách nhiệm:**
- Xác thực và phân quyền người dùng.
- Quản lý thông tin cá nhân (profile) của người dùng.
- Đăng ký và quản lý phương tiện (xe).
- Tạo và xác thực JWT token.

#### 2. Station Service (`station.api`)
**Trách nhiệm:**
- Quản lý trạm sạc.
- Theo dõi trạng thái của các cổng sạc (connector).
- Quản lý vị trí và thông số kỹ thuật của trạm.
- Các nghiệp vụ quản trị trạm.

#### 3. Session Service (`session.api`)
**Trách nhiệm:**
- Quản lý vòng đời của một phiên sạc.
- Giám sát phiên sạc theo thời gian thực.
- Hệ thống đặt chỗ trước.
- Sử dụng WebSocket để cập nhật trạng thái trực tiếp.

#### 4. Billing Service (`billing.api`)
**Trách nhiệm:**
- Tạo và quản lý hóa đơn.
- Xử lý thanh toán.
- Quản lý các gói dịch vụ (subscription).
- Lịch sử thanh toán của người dùng.

#### 5. Staff Service (`staff.api`)
**Trách nhiệm:**
- Các nghiệp vụ dành cho nhân viên.
- Quản lý phiên sạc từ góc độ quản trị.
- Báo cáo vận hành cho nhân viên.

#### 6. Reporting Service (`reporting.api`)
**Trách nhiệm:**
- Phân tích kinh doanh và nghiệp vụ (BI & Analytics).
- Thống kê và insight về việc sử dụng.
- Đưa ra các đề xuất (recommendation).
- Tổng hợp dữ liệu từ các service khác.

## Bắt đầu

### Yêu cầu
- .NET 8.0 SDK
- Docker & Docker Compose
- SQL Server (hoặc PostgreSQL)
- Redis (dùng cho caching và messaging)

### Chạy ứng dụng

1. **Clone repository**
   ```bash
   git clone <repository-url>
   cd EVChargingStation.BE
   ```

2. **Chạy bằng Docker Compose**
   ```bash
   docker-compose up -d
   ```

3. **Chạy riêng lẻ từng service (Môi trường Development)**
   ```bash
   # Chạy các dependencies trước
   docker-compose up -d sqlserver redis
   
   # Chạy từng service
   dotnet run --project services/identity.api
   dotnet run --project services/station.api
   # ... các services khác
   
   # Chạy API Gateway
   dotnet run --project gateway.ocelot
   ```

## Giao tiếp giữa các Service

- **API Gateway**: Tất cả request từ bên ngoài đều đi qua Ocelot gateway.
- **Giao tiếp nội bộ**: Dùng HTTP call cho các tác vụ đồng bộ (synchronous).
- **Giao tiếp hướng sự kiện (Event-driven)**: Dùng Message Queue cho các tác vụ bất đồng bộ (asynchronous).
- **Cập nhật thời gian thực**: Dùng SignalR để giám sát phiên sạc trực tiếp.

## Chiến lược Database

Mỗi service có database riêng theo mô hình database-per-service:
- **Identity DB**: Lưu tài khoản, phương tiện, dữ liệu xác thực.
- **Station DB**: Lưu thông tin trạm, cổng sạc, vị trí.
- **Session DB**: Lưu các phiên sạc, lịch sử và đặt chỗ.
- **Billing DB**: Lưu hóa đơn, thanh toán, gói dịch vụ.
- **Staff DB**: Lưu các nghiệp vụ của nhân viên, lịch sử bảo trì.
- **Reporting DB**: Lưu dữ liệu đã được tổng hợp, phân tích.

## Bảo mật

- Xác thực bằng JWT.
- API Gateway xử lý xác thực/phân quyền.
- Giao tiếp giữa các service được bảo mật.
- Phân quyền dựa trên vai trò (RBAC).

## Khả năng mở rộng

- Các service có thể được scale độc lập.
- Cân bằng tải (Load balancing) qua API Gateway.
- Có khả năng dùng Database sharding.
- Dùng Redis để caching.
- Kiến trúc hướng sự kiện giúp giảm sự phụ thuộc.

## Development

Mỗi service có thể được deploy độc lập và tuân thủ:
- Nguyên tắc Clean Architecture.
- Áp dụng mô hình CQRS ở những nơi phù hợp.
- Thiết kế hướng miền (Domain-driven design).
- Có unit test và integration test đầy đủ.
