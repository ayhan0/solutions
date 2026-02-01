Interview Case – Java Spring Boot Backend

This project is a backend service developed as part of a technical interview case.
It implements a simple phone book application with CRUD and search capabilities,
using Spring Boot 4, Java 17, PostgreSQL, and Redis.

The project is designed with clean architecture principles and production-ready practices.

🚀 Tech Stack

Java 17

Spring Boot 4.x

Spring Data JPA (Hibernate 7)

PostgreSQL (Dockerized)

Redis (Dockerized, used for caching)

Lombok

Jakarta Validation

Maven

📐 Architecture Overview

The application follows a layered architecture:

Entity Layer
JPA entities extending a common BaseEntity
(id, audit fields, active flag)

Repository Layer
Uses Spring Data JPA with interface-based projections for read operations
(prevents unnecessary entity hydration and improves performance)

Service Layer
Contains business logic, transaction management, validation, and caching rules

Web Layer (Controller)
Exposes RESTful APIs and handles request/response mapping

Infrastructure

PostgreSQL for persistence

Redis for caching read-heavy operations

🗄️ Database & Cache Setup (Docker)

PostgreSQL and Redis are provided via Docker.

Why a custom PostgreSQL port?

Local development environments often already have PostgreSQL installed
(e.g. via pgAdmin). To avoid port conflicts, Docker PostgreSQL is exposed
on a non-default port.

Docker Services
Service	Host	Port
PostgreSQL	127.0.0.1	15432
Redis	127.0.0.1	6379

Start services:

docker compose up -d

⚙️ Application Configuration

Configuration is done via application.properties.

Key points:

PostgreSQL datasource (Docker DB on port 15432)

Redis connection for caching

Redis-backed Spring Cache with TTL = 5 minutes

open-in-view disabled (production best practice)

Explicit Hibernate dialect for Spring Boot 4 / Hibernate 7 compatibility

🧩 Core Features

Add a record
(name, surname, age, email, phone)

List all active records (paged)

Search records by name and/or phone

Soft delete records using an active flag

Redis caching for list and search operations

Global exception handling with proper HTTP status codes

🔌 REST API Endpoints
➕ Add Record
POST /api/entity/add

📄 List Records
GET /api/entity/list?page=0&size=50

🔍 Search Records
GET /api/entity/search?name=ali&phone=555

❌ Delete Record (by name or phone)
DELETE /api/entity/delete/{key}


Notes

Phone number is treated as unique

If deleting by name and multiple records match,
the API returns 409 Conflict

🧠 Performance & Design Decisions

Projection-based queries instead of fetching full entities
(reduces memory usage and improves query performance)

DTOs only are exposed to the API layer
(entities are never leaked outside the domain layer)

Redis caching is applied to read-heavy endpoints
(list & search operations)

Soft delete is used instead of physical deletion
(auditability and safer data handling)

Manual mapping is preferred over MapStruct
(single-purpose, low-complexity mappings)

Spring Data Redis built-in JSON serializer is used
(avoids deprecated APIs and keeps configuration future-proof)

▶️ How to Run
1️⃣ Start Docker services
docker compose up -d

2️⃣ Run the Spring Boot application
mvn spring-boot:run

3️⃣ Access the APIs
http://localhost:8080

🧪 Development Notes

Dockerized PostgreSQL runs on port 15432 to avoid conflicts
with locally installed PostgreSQL instances.

Redis is used strictly for read caching, not as a primary data store.

The project structure and configuration are intentionally kept simple
while still demonstrating production-grade practices.