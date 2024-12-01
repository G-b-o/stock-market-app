# Market app

## Database
In order to connect to the database we need to add a connection string to the secret manager using those commands:
```shell
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:Default" <YOUR CONNECTION STRING>
```