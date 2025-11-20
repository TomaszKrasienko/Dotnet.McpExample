# Dotnet.McpExample

A comprehensive .NET implementation of a Model Context Protocol (MCP) server demonstrating how to build AI-powered tools and prompts for order management systems.

## Overview

This project showcases a production-ready MCP server implementation using the `ModelContextProtocol` NuGet package. It demonstrates how to expose business logic, database operations, and external API integrations to AI clients (like Claude) through MCP's standardized protocol.

The example implements a complete order management system with contractors, orders, invoices, and supplier integrations - all accessible through MCP tools (actions) and prompts (read-only queries).

## Features

### MCP Tools (Actions)
- **Contractor Management**: Create and delete contractors
- **Order Management**: Create orders, add order positions, delete orders
- **Invoice Management**: Generate invoices from orders
- **Data Seeding**: Populate database with test data (2 contractors, 60 orders) or clear all data

### MCP Prompts (Read-Only Queries)
- **Contractors**: Retrieve all contractors with details
- **Orders**: Get all orders with their positions
- **Invoices**: Fetch all invoices with line items
- **Suppliers**: Query external supplier product catalogs

## Architecture

The project follows Clean Architecture principles:

```
src/
├── Dotnet.Mcp.Example.Core/          # Core business logic
│   ├── Domain/                        # Entities and constants
│   ├── Application/                   # Services, DTOs, and interfaces
│   └── Infrastructure/                # EF Core, migrations, implementations
├── Dotnet.McpExample.App/             # MCP server application
│   └── Bootstrapper/                  # MCP tools and prompts
├── Dotnet.McpExample.Api/             # REST API for testing
├── Dotnet.McpExample.SupplierApi.1/   # External supplier API (mock)
├── Dotnet.McpExample.SupplierApi.2/   # External supplier API (mock)
└── Dotnet.McpExample.ProductSeed/     # Shared product test data
```

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started) (for PostgreSQL and supplier APIs)
- An MCP client (e.g., [Claude Desktop](https://claude.ai/download))

## Getting Started

### 1. Start Infrastructure Services

Start PostgreSQL and the supplier APIs:

```bash
docker-compose up -d
```

This will start:
- PostgreSQL on `localhost:5432`
- Supplier API 1 on `localhost:5001`
- Supplier API 2 on `localhost:5002`

### 2. Apply Database Migrations

```bash
cd src/Dotnet.Mcp.Example.Core
dotnet ef database update
```

### 3. Publish the MCP Server

```bash
cd src/Dotnet.McpExample.App
dotnet publish -c Release -o ./Publish
```


### 4. Configure Your MCP Client
Add the server to your MCP client configuration (e.g., Claude Desktop):

```json
{
  "mcpServers": {
    "dotnet_days_example": {
      "command": ".../Dotnet.McpExample/src/Dotnet.McpExample.App/Publish/Dotnet.McpExample.App"
    }
  }
}
```
The server uses stdio transport and is now ready to accept MCP client connections.

## Testing with REST API

Alternatively, test the business logic using the REST API:

```bash
cd src/Dotnet.McpExample.Api
dotnet run
```

Access Swagger UI at: `http://localhost:5000/swagger`

### Example API Endpoints

- `POST /api/contractors` - Create a contractor
- `POST /api/orders` - Create an order
- `POST /api/orders/positions` - Add position to order
- `GET /api/orders` - List all orders
- `DELETE /api/orders/{id}` - Delete an order
- `POST /api/invoices` - Create invoice from order

## Project Structure

### Domain Entities

- **Contractor**: Vendors/suppliers with soft-delete support
- **Order**: Purchase orders with status tracking (Open → Confirmed → Realized)
- **OrderPosition**: Line items in orders
- **Invoice**: Billing records created from orders
- **InvoicePosition**: Line items in invoices

### MCP Implementation Details

The MCP server is configured in `Program.cs`:

```csharp
builder.Services
    .AddMcpServer()                    // Add MCP server
    .WithStdioServerTransport()        // Use stdio for communication
    .WithToolsFromAssembly()           // Auto-discover [McpServerTool] methods
    .WithPromptsFromAssembly();        // Auto-discover [McpServerPrompt] methods
```

Tools and prompts are auto-discovered using attributes:
- `[McpServerToolType]` - Marks a class containing tools
- `[McpServerTool]` - Marks a method as an MCP tool
- `[McpServerPrompt]` - Marks a method as an MCP prompt

## Technologies Used

- **.NET 9.0**
- **ModelContextProtocol** (0.4.0-preview.2) - MCP protocol implementation
- **Entity Framework Core 9.0** - ORM
- **PostgreSQL** - Database
- **Npgsql** - PostgreSQL provider
- **ASP.NET Core** - REST API and supplier APIs
- **Docker Compose** - Service orchestration

## Database Configuration

Connection string (configurable in `appsettings.json`):
```
Host=localhost;Port=5432;Database=mcpServer;Username=postgres;Password=postgres
```

## Example Usage with AI

Once connected to an MCP client like Claude, you can ask natural language questions:

- "Create a contractor named 'Tech Supplies Inc'"
- "Show me all current orders"
- "Create an order for contractor X with 5 laptops"
- "Generate an invoice from order Y"
- "What products are available from suppliers?"
- "Seed the database with test data"

The MCP server will automatically translate these requests into the appropriate tool calls or prompt queries.

## Scenario 1

- Use my Dotnet Days example server to clear the data.
- Use my Dotnet Days example server to seed the data.
- Create a new contractor named **Inetum**.
- What data do you need to create a new order and its positions?
- Okay, for our new contractor, add a new order with two positions:
  - Logitech MX Keys, 4 pcs, 400
  - NuPhy Air75, 7 pcs, 600
- Confirm the order.
- Confirm the order again.
- Now, based on this order, create an invoice.
- Add **get_orders_text** with the prompt: "Get today's invoices."

## Scenario 2

- Add **get_invoices_text**,**get_contractors_text**
- Can you give me summarized quantity of invoices grouped by month and contractor with names?
- Okay, so now give me all invoices of ... in pdf table

### Scenario 3
- Add **get_supplier_products**.
- Add **get_supplier_products** again.
- Compare the products and create orders for both suppliers with the five most cost-efficient products.

