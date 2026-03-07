
## Yêu cầu hệ thống

- **Node.js** (phiên bản 18+)
- **.NET 8 SDK**
- **Docker** (để chạy SQL Server)
- **SQL Server** (hoặc dùng Docker)

---

## Cách chạy ứng dụng

### Bước 1: Khởi tạo SQL Server

Chạy SQL Server bằng Docker:

```bash
cd netcore-server
docker-compose up -d
```

Thông tin kết nối:
- Database: `AuthDB`
- Host: `localhost`
- Port: `1433`
- User: `sa`
- Password: `YourStrong@Pass123`

> **Lưu ý:** đảm bảo chuỗi kết nối trong `netcore-server/appsettings.json` khớp với cấu hình của bạn.

### Bước 2: Cập nhật Database (Migration)

```bash
cd netcore-server
dotnet ef database update
```

### Bước 3: Chạy Backend API

```bash
cd netcore-server
dotnet run
```

- API: `http://localhost:5210`
- Swagger UI: `http://localhost:5210/swagger`
- Base path: `/api/v1`

### Bước 4: Chạy Frontend

Mở terminal mới:

```bash
cd fe-dashboard
npm install
npm run dev
```

- Frontend: `http://localhost:5173`
- API URL (trong `.env`): `http://localhost:5210/api/v1`

