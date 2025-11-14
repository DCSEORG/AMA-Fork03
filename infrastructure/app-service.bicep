// Azure App Service deployment for Expense Management Application
// Bicep template for deploying a low-cost development App Service in UKSOUTH

@description('Name of the App Service')
param appName string

@description('Location for all resources')
param location string = 'uksouth'

@description('App Service Plan SKU')
param sku string = 'F1'

@description('App Service Plan name')
param appServicePlanName string = '${appName}-plan'

// Create App Service Plan with low-cost development SKU
resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: sku
    tier: 'Free'
    size: sku
    family: 'F'
    capacity: 1
  }
  properties: {
    reserved: false
  }
  kind: 'app'
}

// Create App Service
resource appService 'Microsoft.Web/sites@2022-03-01' = {
  name: appName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      netFrameworkVersion: 'v8.0'
      metadata: [
        {
          name: 'CURRENT_STACK'
          value: 'dotnet'
        }
      ]
      appSettings: [
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: 'Development'
        }
      ]
    }
  }
}

// Outputs
output appServiceName string = appService.name
output appServiceUrl string = 'https://${appService.properties.defaultHostName}'
output appServicePlanName string = appServicePlan.name
