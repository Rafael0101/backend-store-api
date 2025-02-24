# Backend Store API

This project is a test for a backend developer position, representing a Web API to manage orders for a store. The API is built using ASP.NET Core and Entity Framework Core, with MySQL as the database.

## Prerequisites

- .NET 6 or higher
- MySQL Server
- Entity Framework Core CLI (for migrations)

## Getting Started

1. Clone the repository: git clone https://github.com/Rafael0101/backend-store-api.git
2. Navigate to the project directory: cd backend-store-api
3. Configure the database connection in the appsettings.json file
4. Apply the database migrations: dotnet ef migrations add InitialCreate, dotnet ef database update
5. Run the application: dotnet run

## Running Tests

To run the tests, use the following command: dotnet test

## Configuring Environment Variables

To connect to the database, the application requires the following environment variables to be set:

- DB_USERNAME: the username for the database
- DB_PASSWORD: the password for the database
- DB_URL: the URL for the database

To set these environment variables, follow these steps:

1. Open a terminal window.
2. Set the DB_USERNAME, DB_PASSWORD, and DB_URL environment variables using the appropriate values for your database:
```
    export DB_USERNAME=your-username
    export DB_PASSWORD=your-password
    export DB_URL=Server=localhost;Database=StoreDB;
```
   
  Note: Replace the values root, root, and StoreDB with your own values.

3. Run the application as described in the "Getting Started" section above.
   
## API Endpoints

- GET /api/orders: List all orders.
- GET /api/orders/{id}: Get an order by ID.
- POST /api/orders: Create a new order.
- PUT /api/orders/{id}: Update an order.
- DELETE /api/orders/{id}: Delete an order.
- POST /api/orders/{orderId}/products: Add a product to an order.
- DELETE /api/orders/{orderId}/products/{productId}: Remove a product from an order.

## Disclaimer

Este README foi escrito em inglês para fim de boas práticas.

Qualquer dúvida sobre este projeto, bem como instalação e uso fique a vontade para me contatar aqui ou por email: rafanoliveira13@gmail.com.
