#!/bin/bash

# App Modernization Deployment Script
# This script deploys all infrastructure and application code to Azure

set -e

echo "==================================="
echo "App Modernization Deployment"
echo "==================================="
echo ""

# Variables - Update these as needed
RESOURCE_GROUP="ExpenseManagement-RG"
LOCATION="uksouth"
APP_NAME="expensemanagement-app-$RANDOM"

echo "Configuration:"
echo "  Resource Group: $RESOURCE_GROUP"
echo "  Location: $LOCATION"
echo "  App Name: $APP_NAME"
echo ""

# Check if logged in to Azure
echo "Checking Azure CLI login status..."
az account show > /dev/null 2>&1 || { echo "Please run 'az login' first"; exit 1; }

# Create Resource Group
echo "Creating resource group..."
az group create --name $RESOURCE_GROUP --location $LOCATION

# Deploy App Service infrastructure
echo "Deploying App Service infrastructure..."
az deployment group create \
  --resource-group $RESOURCE_GROUP \
  --template-file ./infrastructure/app-service.bicep \
  --parameters appName=$APP_NAME location=$LOCATION

# Build and deploy application code
echo "Building application..."
cd ./src/ExpenseManagementApp
dotnet publish -c Release -o ./publish

# Create deployment package
echo "Creating deployment package..."
cd ./publish
zip -r ../../../app.zip .
cd ../../..

# Deploy application to App Service
echo "Deploying application to Azure App Service..."
az webapp deploy \
  --resource-group $RESOURCE_GROUP \
  --name $APP_NAME \
  --src-path ./app.zip

echo ""
echo "==================================="
echo "Deployment Complete!"
echo "==================================="
echo ""
echo "Your application is available at:"
echo "https://$APP_NAME.azurewebsites.net/Index"
echo ""
echo "Note: Navigate to /Index to view the application"
echo ""
