# migraciones

dotnet ef migrations add InitialCreate --project ../Catalog.Infrastructure --startup-project .
dotnet ef database update --project ../Catalog.Infrastructure --startup-project .


# powershell development variable
$env:ASPNETCORE_ENVIRONMENT="Development"