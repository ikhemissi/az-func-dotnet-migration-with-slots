# Migration from .NET6 in-process to .NET8 isolated

**Disclaimer**: this is an experimental project and should not be used as-is in production

The goal of this project is to experiment with the migration of functions in .NET in-process to .NET8 isolated using deployment slots as described in [this migration guide](https://learn.microsoft.com/en-us/azure/azure-functions/migrate-dotnet-to-isolated-model?tabs=net8#update-your-function-app-in-azure)

## Deploy the infrastructure

```sh
azd provision
```

## Deploy the function in .NET6 in-process to the production slot

Deploy the initial version of the Function App written in .NET6 to the production slot.

A sample code was provided in the [ApiInDotNet6InProcess](./ApiInDotNet6InProcess) directory.

```sh
func azure functionapp publish $FUNCTION_APP_NAME --dotnet
```

Test the `Ping` function (on the main/production slot) and ensure it returns `PONG .NET6.0`.

## Deploy the function in .NET8 Isolated to the staging slot

Deploy the migrated version of the Function App written in .NET8 to the staging slot.

A sample code was provided in the [ApiInDotNet8Isolated](./ApiInDotNet8Isolated) directory.

```sh
func azure functionapp publish $FUNCTION_APP_NAME --dotnet-isolated --slot staging
```

Test the `Ping` function of the staging slot and ensure it returns `PONG .NET8.0`.

## Swap slots

```sh
az functionapp deployment slot swap --name $FUNCTION_APP_NAME --resource-group $RESOURCE_GROUP --slot staging --target-slot production
```

Test the `Ping` function on the production slot and ensure it returns `PONG .NET8.0`.

## Delete the resource group

Don't forget to delete the resource group once you have finished testing the migration with slot swapping:

```sh
azd down
```

