# Todo List API

A simple REST API developed using ASP.NET Core and Entity Framework Core to manage a to-do list system. This API includes functionalities for user authentication using JWT tokens, and CRUD operations for managing tasks with specific validations.

## Project Structure

- **Controllers**: Manages the API endpoints for authentication and tasks.
- **Models**: Defines the data structures and validation rules.
- **Data**: Configures the in-memory database for development/testing.
- **Tests**: Unit tests for the API endpoints using xUnit.

## Requirements

- **ASP.NET Core** (latest stable version)
- **Entity Framework Core** (latest stable version)
- **In-Memory Database** for development/testing
- **JWT Authentication**

## Setup Instructions

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed
- **Git**

### Installation

1. **Clone the Repository**
   ```bash
   git clone https://github.com/Fitz-Roy/Fitz-Roy.git
   cd Fitz-Roy
2. **Restore Packages**
   ```bash
   dotnet restore
3. **Build the project**
   ```bash
   dotnet build
4. **Run the API**
   ```bash
   dotnet run

### Configuration

1. JWT Configuration: Ensure you set up a JWT authentication key in appsettings.json or through environment variables for securing the API.
2. Swagger: Swagger UI is enabled in development mode for API documentation. Access it at http://localhost:<port>/swagger after running the API.

### Authentication

This API uses JWT tokens for authentication. To use the endpoints, you need to generate a token using the /api/auth/login endpoint with valid credentials.

## Sample Credentials:
- **Username: admin**
- **Password: password**

## Token Generation:
- **Send a POST request to /api/auth/login with the sample credentials to receive a JWT token.**
- **Use this token to authenticate other requests by adding it to the Authorization header as Bearer <token>.**

### API Endpoints

## Authentication
- **POST /api/auth/login: Authenticate a user and receive a JWT token.**

## Tarea Management
- **GET /api/tareas: Retrieve all tareas.**
- **GET /api/tareas/{id}: Retrieve a specific tarea by ID.**
- **POST /api/tareas: Create a new task.**
- **PUT /api/tareas/{id}: Update an existing tarea.**
- **DELETE /api/tareas/{id}: Delete a task by ID.**

### Validations

- **Tarea title cannot be empty and must be a maximum of 100 characters.**
- **Description should not exceed 500 characters.**
- **Tarea status must be either "pendiente", "en progreso", or "completada".**

### Testing

## This project includes unit tests for the API endpoints using xUnit.


1. **Run Test**
   ```bash
   dotnet test
2. Check Results: The test output will show which tests passed or failed.

### In-Memory Database

## The API uses an in-memory database for easy testing and development. This means all data will reset each time the application is restarted. No additional setup is required for this database.
