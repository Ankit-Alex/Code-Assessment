# Product Management API

A .NET Core Web API solution for managing products with distributed ID generation support. This solution follows clean architecture principles and provides a robust product management system.

## Architecture

The solution is structured into multiple projects following clean architecture principles:

- **API**: Web API project containing controllers and API configuration
- **Core**: Contains business logic, interfaces, and DTOs
- **DataAccess**: Implements data access and persistence
- **Domain**: Contains domain entities and business rules

## Features

- CRUD operations for products
- Distributed product ID generation with node support
- Automatic ID padding and validation
- Clean architecture implementation
- Repository pattern for data access
- Result pattern for error handling

## Technical Stack

- .NET Core 7.0+
- Entity Framework Core
- PostgreSQL (for database)
- Clean Architecture
- Repository Pattern
- REST API

## Getting Started

### Prerequisites

- .NET Core SDK 7.0 or later
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
2. Set DataAccess as the default project
3. Run migrations:
```powershell
Update-Database
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

## API Endpoints

### Products

- **GET** `/api/products` - Get all products
- **GET** `/api/products/{id}` - Get product by ID
- **POST** `/api/products` - Create new product
- **PUT** `/api/products/{id}` - Update product
- **DELETE** `/api/products/{id}` - Delete product
- **PATCH** `/api/products/{id}/stock` - Update product stock

### Product ID Format

Product IDs follow a specific format:
- 6-digit numeric format
- First digit: Node ID (0-9)
- Last 5 digits: Sequential number
- Shorter IDs are padded with leading zeros

Example: "000123" (Node 0, Sequence 123)

## Error Handling

The API uses a Result pattern for consistent error handling:
- Success responses include the data and success status
- Error responses include error messages and failure status
- Validation errors return appropriate error messages

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details. 