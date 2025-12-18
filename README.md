# API для управления задачами под `.net`
## Реализовано 
1. CRUD
2. Id типов задач от 1 до 4 
3. Статусы задач: inProcess, completed, failed

Команда для создания бд solution-а:

`dotnet ef database update --project TaskManagerServer.App.Api\TaskManagerServer.App.Api.csproj -c DatabaseContext --verbose`

## Основные фреймворки
1. .NET 8
2. ASP.NET Core
3. Entity Framework Core

## База данных
1. БД	        PostgreSQL 12.18
2. Клиент БД	Npgsql 8.0
3. Миграции	EF Core Migrations