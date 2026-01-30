@echo off
echo ============================================================
echo  Scaffolding DbContext and Entities
echo ============================================================

dotnet ef dbcontext scaffold "Name=ConnectionStrings:FoodDeliveryDb" Microsoft.EntityFrameworkCore.SqlServer ^
 -o Entities ^
 --context FoodDeliveryDbContext ^
 --context-dir Data ^
 --project ..\src\FoodDelivery.Domain\FoodDelivery.Domain.csproj ^
 --startup-project ..\src\FoodDelivery.API\FoodDelivery.API.csproj ^
 --force

echo.
echo ============================================================
echo  Scaffolding completed successfully.
echo ============================================================

pause