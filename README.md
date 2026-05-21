# 📚 API Bookstore

Dự án RESTful Web API cho hệ thống quản lý cửa hàng sách trực tuyến, xây dựng bằng ASP.NET Core. Đây là dự án cá nhân nhằm thực hành backend development và cải thiện kĩ năng lập trình.

---

## 🛠️ Tech Stack

- **Framework:** ASP.NET Core Web API
- **ORM:** Entity Framework Core
- **Database:** SQL Server
- **Authentication:** JWT (JSON Web Token)
- **API Docs:** Swagger / OpenAPI

---

## 📁 Kiến trúc dự án

Dự án áp dụng **Layered Architecture** kết hợp **Repository Pattern** và **Service Pattern**.

```
Controllers/        → Xử lý HTTP request/response
Services/           → Business logic
Repositories/       → Tương tác database (EF Core)
Models/
  ├── Entities/     → Class ánh xạ với bảng database
  └── DTOs/         → Class dùng cho request/response
Data/               → DbContext, Migration
Middleware/         → Global Exception Handler
Helpers/            → JWT Helper, Password Helper
Configurations/     → JWT Settings, CORS
```

---

## ✨ Tính năng chính

- 🔐 **Authentication** — Đăng ký, đăng nhập, JWT Token
- 📖 **Quản lý sách** — CRUD sách, tìm kiếm, lọc theo danh mục, phân trang
- 🗂️ **Quản lý danh mục** — CRUD danh mục sách
- 👤 **Quản lý người dùng** — Xem, cập nhật, xoá tài khoản (Admin)
- 🛒 **Hệ thống đơn hàng** — Đặt hàng, lịch sử đơn hàng, cập nhật trạng thái

---
 
## 👨‍💻 Tác giả
 
Dự án được xây dựng với mục đích học tập và thực hành backend development.

## ⚙️ Cài đặt & Chạy dự án

### Yêu cầu

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) hoặc VS Code

### Các bước

**1. Clone repo**
```bash
git clone https://github.com/<your-username>/API_Bookstore.git
cd API_Bookstore
```

**2. Cấu hình connection string**

Mở file `appsettings.json`, cập nhật thông tin database:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=BookStoreDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key-at-least-32-characters",
    "Issuer": "API_Bookstore",
    "Audience": "API_Bookstore_Client",
    "ExpiresInHours": 24
  }
}
```

**3. Tạo database**
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

**4. Chạy project**
```bash
dotnet run
```

**5. Mở Swagger UI**
```
https://localhost:<port>/swagger
```

---

## 🔑 Phân quyền

| Chức năng | Anonymous | User | Admin |
|---|:---:|:---:|:---:|
| Xem sách / danh mục | ✅ | ✅ | ✅ |
| Tạo / Sửa / Xoá sách | ❌ | ❌ | ✅ |
| Đặt hàng | ❌ | ✅ | ✅ |
| Xem đơn hàng của mình | ❌ | ✅ | ✅ |
| Quản lý tất cả đơn hàng | ❌ | ❌ | ✅ |
| Quản lý người dùng | ❌ | ❌ | ✅ |

---

## 📌 Trạng thái dự án

```
✅ Phase 1 — Project Setup
✅ Phase 2 — Database Design
✅ Phase 3 — CRUD APIs (Category, Book, User) - (Pagination, Search, Sorting)
✅ Phase 4 — Authentication & Authorization (JWT)
✅ Phase 5 — Order System
```

---

## 👨‍💻 Tác giả
* **Sángg** - Fullstack Developer & System Architect.
* Dự án được xây dựng với mục đích học tập và thực hành backend development.
