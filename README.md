# Market app

## Database
In order to connect to the database we need to add a connection string to the secret manager via those commands:
```shell
dotnet user-secrets init
dotnet user-secrets set "ConnectionString:Default" <YOUR CONNECTION STRING>
```