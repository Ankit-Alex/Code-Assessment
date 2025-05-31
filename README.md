# Product Management API

A .NET Core Web API solution for managing products with distributed ID generation support. This solution follows N-Tier architecture principles and implements a robust error handling pattern.

## Architecture

The solution follows a traditional N-Tier architecture with clear separation of concerns:

- **Presentation Tier (API)**: Web API project containing controllers and endpoints
- **Business Logic Tier (Core)**: Contains business logic, interfaces, DTOs, and the Result pattern implementation
- **Data Access Tier (DataAccess)**: Implements data access patterns and database operations
- **Tests**: Contains unit tests for the business logic

Each tier has specific responsibilities:
- Presentation Tier handles HTTP requests, routing, and API documentation
- Business Logic Tier manages business rules, validation, and orchestration
- Data Access Tier handles database operations and data persistence

## Features

- CRUD operations for products with proper error handling
- Distributed product ID generation with node support
- Automatic ID validation and zero-padding
- N-Tier architectural separation
- Generic Repository pattern for data access
- Result pattern for consistent error handling
- Input validation and sanitization
- PostgreSQL sequence-based ID generation
- Comprehensive unit test coverage

## Getting Started

### Prerequisites

- .NET Core SDK 8.0 or later
- PostgreSQL database server
- Visual Studio 2022 or VS Code

### Configuration

1. Update the database connection string in `API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your_Connection_String_Here"
  }
}
```

2. Configure the distributed system options in `appsettings.json`:
```json
{
  "DistributedSystemOptions": {
    "NodeId": "0"  // Set unique node ID (0-9) for distributed environments
  }
}
```

### Database Setup

1. Open Package Manager Console
2. Set API as the default project
3. Run migrations:
```bash
dotnet ef database update
```

### Running the Application

1. Clone the repository
2. Navigate to the solution directory
3. Restore dependencies:
```bash
dotnet restore
```
4. Run the application:
```bash
dotnet run --project API
```

### Running Tests

To run the unit tests:

```bash
dotnet test
```

## Testing Approach

The solution includes comprehensive unit tests using:
- **xUnit**: Testing framework
- **NSubstitute**: Mocking framework
- **FluentAssertions**: Fluent assertion library

Test coverage includes:
- Product creation validation
- ID formatting and validation
- Stock management operations
- Product updates and deletions
- Error handling scenarios
- Edge cases for product IDs

Key test scenarios:
- Valid and invalid product ID formats
- Duplicate product name validation
- Stock increment/decrement operations
- Product CRUD operations
- Error conditions and edge cases

## API Endpoints

### Products

- **GET** `/api/products` - Get all products
- **GET** `/api/products/{id}` - Get product by ID
- **POST** `/api/products` - Create new product
- **PUT** `/api/products/{id}` - Update product
- **DELETE** `/api/products/{id}` - Delete product
- **PATCH** `/api/products/{id}/stock` - Update product stock

All endpoints return a Result object with the following structure:
```json
{
  "isSuccess": boolean,
  "data": object | null,
  "error": string | null
}
```

### Product ID Format

Product IDs follow a specific format:
- 6-digit numeric format
- First digit: Node ID (0-9)
- Last 5 digits: Sequential number
- Shorter IDs are automatically padded with leading zeros
- Only numeric characters are allowed

Examples:
- "000123" (Node 0, Sequence 123)
- "100001" (Node 1, Sequence 1)
- Input "123" is automatically padded to "000123"

## Architectural Layers

### Presentation Tier (API)
- Handles HTTP requests and responses
- Implements API endpoints and routing
- Manages request/response serialization
- Handles basic request validation
- API documentation (Swagger/OpenAPI)

### Business Logic Tier (Core)
- Implements business rules and validation
- Manages data transfer objects (DTOs)
- Handles service orchestration
- Implements the Result pattern for error handling
- Defines interfaces for dependency injection

### Data Access Tier (DataAccess)
- Implements the Repository pattern
- Manages database connections
- Handles entity mapping
- Implements database migrations
- Manages database transactions

## Error Handling

The API implements a consistent error handling pattern using the Result<T> class:
- Success responses include the data and success status
- Error responses include detailed error messages
- Common validation errors:
  - Invalid product ID format
  - Product not found
  - Duplicate product names
  - Invalid input data

## Data Access

The solution uses:
- Generic Repository pattern for data access abstraction
- Entity Framework Core for database operations
- PostgreSQL sequences for distributed ID generation
- Async operations for better performance
