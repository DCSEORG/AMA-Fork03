# Expense Management System - Deployment Guide

## Quick Start Deployment

This application modernizes a legacy Expense Management System into a cloud-native Azure application.

### Prerequisites

- Azure CLI installed and logged in
- .NET 8.0 SDK installed
- Bash shell (Linux, macOS, or WSL on Windows)

### One-Line Deployment

After cloning this repository:

1. Login to Azure:
   ```bash
   az login
   az account set --subscription "<your-subscription-id>"
   ```

2. Run the deployment script:
   ```bash
   ./deploy.sh
   ```

### Accessing the Application

After deployment completes, the script will display your application URL. 

**Important:** The application uses Razor Pages, so you must navigate to:
```
https://<your-app-name>.azurewebsites.net/Index
```

Do **not** just navigate to the root URL. Add `/Index` to access the application.

### Application Features

The modernized application includes:

1. **Add Expense** (`/Index`) - Submit new expense entries
2. **Expenses** (`/Expenses`) - View and filter all expenses  
3. **Approve Expenses** (`/Approve`) - Review and approve pending expenses
4. **API Documentation** (`/swagger`) - Swagger UI for testing REST APIs

### REST API Endpoints

All API endpoints are documented in Swagger and accessible at:
```
https://<your-app-name>.azurewebsites.net/swagger
```

Available endpoints:
- `GET /api/expenses` - Get all expenses (with optional filter)
- `GET /api/expenses/{id}` - Get specific expense
- `GET /api/expenses/pending` - Get pending expenses (with optional filter)
- `POST /api/expenses` - Create new expense
- `PUT /api/expenses/{id}` - Update expense
- `PUT /api/expenses/{id}/approve` - Approve expense
- `DELETE /api/expenses/{id}` - Delete expense

**Note:** Currently, all endpoints return dummy data for demonstration purposes. Database integration will be added in a future update.

### Manual Deployment Steps

If you prefer to deploy manually:

1. **Build and publish the application:**
   ```bash
   cd src/ExpenseManagementApp
   dotnet publish -c Release -o ./publish
   ```

2. **Create deployment package:**
   ```bash
   cd ./publish
   zip -r ../../../app.zip .
   cd ../../..
   ```

3. **Deploy infrastructure:**
   ```bash
   # Create resource group
   az group create --name ExpenseManagement-RG --location uksouth
   
   # Deploy App Service
   az deployment group create \
     --resource-group ExpenseManagement-RG \
     --template-file ./infrastructure/app-service.bicep \
     --parameters appName=<your-unique-app-name> location=uksouth
   ```

4. **Deploy application:**
   ```bash
   az webapp deploy \
     --resource-group ExpenseManagement-RG \
     --name <your-unique-app-name> \
     --src-path ./app.zip
   ```

### Infrastructure Details

- **Region:** UK South (uksouth)
- **App Service Plan SKU:** F1 (Free tier) for development
- **Runtime:** .NET 8.0
- **HTTPS:** Enforced

### Development

To run locally:
```bash
cd src/ExpenseManagementApp
dotnet run
```

Then navigate to `https://localhost:5001/Index` or `http://localhost:5000/Index`

### Technology Stack

- ASP.NET Core 8.0 Razor Pages
- REST API with Controllers
- Swagger/OpenAPI documentation
- Bootstrap 5 for UI
- Azure App Service
- Bicep for Infrastructure as Code
