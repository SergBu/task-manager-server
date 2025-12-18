# API для управления задачами под `.net`
## Для задач реализованы: 
1. CRUD
2. Id типов задач от 1 до 4 
3. Статусы задач: inProcess, completed, failed

Команда для создания бд solution-а:

`dotnet ef database update --project TaskManagerServer.App.Api\TaskManagerServer.App.Api.csproj -c DatabaseContext --verbose`
