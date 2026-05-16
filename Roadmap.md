# API_Bookstore Development Roadmap

## Phase 1 — Project Setup

### Goal

Initialize the project and prepare the development environment.

### Tasks

* Create ASP.NET Core Web API project
* Setup SQL Server connection
* Install required NuGet packages
* Configure Swagger
* Configure Entity Framework Core
* Setup Dependency Injection
* Create folder structure

### Technologies

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Swagger

### Expected Result

* Project runs successfully
* Swagger UI works
* Database connection works

---

# Phase 2 — Database Design

### Goal

Design the bookstore database.

### Main Tables

## Users

* Id
* Username
* Email
* PasswordHash
* Role

## Categories

* Id
* Name
* Description

## Books

* Id
* Title
* Author
* Price
* Stock
* Description
* ImageUrl
* CategoryId

## Orders

* Id
* UserId
* OrderDate
* TotalPrice

## OrderDetails

* Id
* OrderId
* BookId
* Quantity
* Price

### Tasks

* Create entities
* Configure relationships
* Create migrations
* Update database

### Expected Result

* Database schema completed
* Relationships working correctly

---

# Phase 3 — Basic CRUD APIs

### Goal

Build core CRUD functionalities.

---

## Category API

### Endpoints

* GET /api/categories
* GET /api/categories/{id}
* POST /api/categories
* PUT /api/categories/{id}
* DELETE /api/categories/{id}

### Tasks

* Create DTOs
* Create Repository
* Create Service
* Create Controller

---

## Book API

### Endpoints

* GET /api/books
* GET /api/books/{id}
* POST /api/books
* PUT /api/books/{id}
* DELETE /api/books/{id}

### Additional Features

* Search books
* Filter by category
* Pagination

---

## User API

### Endpoints

* GET /api/users
* GET /api/users/{id}
* PUT /api/users/{id}
* DELETE /api/users/{id}

### Expected Result

* CRUD APIs work correctly
* Data saved to database successfully

---

# Phase 4 — Authentication & Authorization

### Goal

Implement secure authentication system.

### Features

* Register
* Login
* JWT Authentication
* Role-based Authorization

### Roles

* Admin
* Customer

### Tasks

* Password hashing
* Generate JWT token
* Configure authentication middleware
* Protect APIs with [Authorize]

### Example Protected API

```csharp
[Authorize]
[HttpGet]
public IActionResult GetProfile()
{
}
```

### Expected Result

* Users can login/register
* JWT token works correctly
* Protected APIs require authentication

---

# Phase 5 — Advanced Features

### Goal

Add practical real-world features.

---

## Pagination

Example:

```plaintext
GET /api/books?page=1&pageSize=10
```

---

## Search

Example:

```plaintext
GET /api/books?search=harry
```

---

## Sorting

Example:

```plaintext
GET /api/books?sortBy=price
```

---

## File Upload

### Features

* Upload book image
* Save image path to database

---

## Global Exception Middleware

### Goal

Handle errors globally.

### Expected Result

* Clean error responses
* Avoid duplicated try-catch

---

# Phase 6 — Order System

### Goal

Build order management system.

### Features

* Create order
* Add order details
* Calculate total price
* Get order history

### Endpoints

* POST /api/orders
* GET /api/orders
* GET /api/orders/{id}

### Expected Result

* Users can place orders
* Order data saved correctly

---

# Phase 7 — Code Improvement

### Goal

Improve project quality and maintainability.

### Improvements

* Use AutoMapper
* Add FluentValidation
* Use Generic Repository
* Use Response Wrapper
* Add Logging
* Add async/await everywhere

### Expected Result

* Cleaner architecture
* More scalable project

---

# Phase 8 — Testing

### Goal

Test API functionality.

### Tools

* Postman
* Swagger

### Tasks

* Test all endpoints
* Test authentication
* Test validation
* Test error handling

### Expected Result

* APIs stable
* No major bugs

---

# Phase 9 — Deployment

### Goal

Deploy project online.

### Options

* Microsoft Azure
* Render
* Railway
* IIS

### Tasks

* Publish API
* Configure production database
* Configure environment variables

### Expected Result

* Public API accessible online

---

# Final Project Goals

After completing this roadmap, the project should demonstrate:

* Clean ASP.NET Core Web API architecture
* CRUD operations
* Authentication & Authorization
* Entity Framework Core usage
* Repository & Service Pattern
* SQL Server integration
* RESTful API design
* Real-world backend development practices

---

# Suggested Future Improvements

## Intermediate Features

* Refresh Token
* Email verification
* Forgot password
* Shopping cart
* Wishlist

## Advanced Features

* Redis caching
* Docker
* Unit Testing
* Clean Architecture
* CQRS Pattern
* Microservices
* CI/CD pipeline

---