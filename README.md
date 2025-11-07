# AIA.MakeModelStandard

A standardized aircraft make and model list API for the aviation insurance industry. This API provides downloadable AMIN (Aircraft Manufacturer Index Number) data with year, make, model, and related FAA manufacturer model numbers.

## Overview

The AIA Make Model Standard API serves as a centralized reference for aircraft manufacturers and models, ensuring consistency across aviation insurance operations. This helps reduce errors from non-standardized naming and supports underwriting and risk assessment processes.

## Features

- **Multiple Export Formats**: Download AMIN data in JSON or CSV format
- **Flexible Filtering**: Query by year, make, or retrieve all records
- **Azure SQL Database**: Enterprise-grade database hosting with built-in redundancy
- **RESTful API**: Clean, well-documented endpoints following REST principles
- **Swagger Documentation**: Interactive API documentation at the root URL
- **Sample Data**: Pre-seeded with sample aircraft records for testing

## Technology Stack

- **.NET 8.0**: Modern, cross-platform framework
- **ASP.NET Core Web API**: High-performance web framework
- **Entity Framework Core 8.0**: ORM for database operations
- **SQL Server (Azure SQL)**: Cloud-hosted relational database
- **CsvHelper**: CSV generation library
- **Swagger/OpenAPI**: API documentation

## Project Structure

```
AIA.MakeModelStandard.Api/
├── Controllers/
│   └── AminController.cs          # API endpoints for AMIN data
├── Data/
│   └── ApplicationDbContext.cs    # Entity Framework database context
├── Migrations/
│   ├── 20251107000000_InitialCreate.cs
│   └── ApplicationDbContextModelSnapshot.cs
├── Models/
│   └── AminRecord.cs              # AMIN data model
├── Services/
│   └── CsvService.cs              # CSV generation service
├── Program.cs                      # Application entry point
├── appsettings.json               # Configuration (production)
├── appsettings.Development.json   # Configuration (development)
└── AIA.MakeModelStandard.Api.csproj
```

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Azure SQL Database instance
- Visual Studio 2022 or Visual Studio Code (optional)

### Configuration

1. Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tcp:your-server.database.windows.net,1433;Initial Catalog=AIA_MakeModelStandard;Persist Security Info=False;User ID=your-username;Password=your-password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}
```

2. For development, update `appsettings.Development.json` with your development database connection string.

### Database Setup

The application uses Entity Framework Core migrations. On first run in development mode, the database will be automatically created and seeded with sample data.

To manually apply migrations:

```bash
cd AIA.MakeModelStandard.Api
dotnet ef database update
```

### Running the Application

```bash
cd AIA.MakeModelStandard.Api
dotnet restore
dotnet build
dotnet run
```

The API will be available at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

In development mode, Swagger UI will be available at the root URL.

## API Endpoints

### Get All AMIN Records (JSON)
```
GET /api/amin/json
```
Returns all AMIN records in JSON format.

**Response Example:**
```json
[
  {
    "aminNumber": "AMIN-2024-001",
    "year": 2024,
    "make": "Cessna",
    "model": "172 Skyhawk",
    "faaManufacturerModelNumber": "172, 172N, 172P, 172R, 172S"
  }
]
```

### Download AMIN Records (CSV)
```
GET /api/amin/csv
```
Downloads all AMIN records as a CSV file.

**Response:** CSV file named `AMIN_List_YYYYMMDD_HHmmss.csv`

### Get AMIN Records by Year
```
GET /api/amin/json/year/{year}
```
Returns AMIN records filtered by year.

**Example:** `/api/amin/json/year/2024`

### Get AMIN Records by Make
```
GET /api/amin/json/make/{make}
```
Returns AMIN records filtered by manufacturer/make.

**Example:** `/api/amin/json/make/cessna`

### Get Database Statistics
```
GET /api/amin/stats
```
Returns statistics about the AMIN database.

**Response Example:**
```json
{
  "totalRecords": 150,
  "distinctMakes": 25,
  "years": [2020, 2021, 2022, 2023, 2024],
  "yearRange": "2020 - 2024"
}
```

## Data Model

### AminRecord

| Field | Type | Description |
|-------|------|-------------|
| Id | int | Primary key (auto-generated) |
| AminNumber | string(50) | Unique AMIN identifier |
| Year | int | Year of the AMIN record |
| Make | string(100) | Aircraft manufacturer |
| Model | string(100) | Aircraft model |
| FaaManufacturerModelNumber | string(500) | Related FAA model numbers (comma-separated) |
| CreatedDate | DateTime | Record creation timestamp |
| ModifiedDate | DateTime? | Last modification timestamp |

## Development

### Adding New AMIN Records

AMIN records can be added through:
1. Direct database inserts
2. Additional seed data in `ApplicationDbContext.cs`
3. Future API endpoints for CRUD operations

### Creating Migrations

When you modify the data model:

```bash
dotnet ef migrations add YourMigrationName
dotnet ef database update
```

## Deployment

### Azure App Service

1. Create an Azure App Service instance
2. Configure the connection string in Azure App Service Configuration
3. Deploy using Visual Studio, Azure CLI, or GitHub Actions

### Connection String Configuration

For production, store connection strings securely using:
- Azure App Service Configuration
- Azure Key Vault
- Environment variables

Never commit production connection strings to source control.

## Security Considerations

- Connection strings contain sensitive credentials
- Use Azure Managed Identity for production deployments
- Enable HTTPS only in production
- Configure CORS policies appropriately
- Implement authentication/authorization as needed

## License

[Add your license information here]

## Contact

[Add contact information here]

## Contributing

[Add contribution guidelines here]