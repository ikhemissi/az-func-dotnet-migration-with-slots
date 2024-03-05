targetScope = 'subscription'

@minLength(1)
@maxLength(64)
@description('Name which is used to generate a short unique hash for each resource')
param name string

@minLength(1)
@description('Primary location for all resources')
param location string

var resourceToken = toLower(uniqueString(subscription().id, name, location))
var tags = { 'azd-env-name': name }

var prefix = '${name}-${resourceToken}'

resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: '${name}-rg'
  location: location
  tags: tags
}

module storageAccount './storageaccount.bicep' = {
  name: 'storageAccount'
  scope: resourceGroup
  params: {
    name: '${toLower(take(replace(prefix, '-', ''), 17))}storage'
    location: location
    tags: tags
  }
}

module functionApp './functionapp.bicep' = {
  name: 'functionapp'
  scope: resourceGroup
  params: {
    name: '${prefix}-func'
    hostingPlanName: '${prefix}-plan'
    location: location
    tags: tags
    azdServiceName: 'api'
    storageAccountName: storageAccount.outputs.storageAccountName
    appInsightsInstrumentationKey: applicationInsights.outputs.instrumentationKey
  }
}

module applicationInsights './appinsights.bicep' = {
  scope: resourceGroup
  name: 'appinsights'
  params: {
    name: '${prefix}-insights'
    location: location
    tags: tags
  }
}
