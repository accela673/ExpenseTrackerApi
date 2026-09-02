# Expense Tracker API

REST API для управления категориями и личными расходами.

### Стэк

* C#
* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* Microsoft SQL Server
* Swagger
* Git

### Архитектура

```text
Controller -> Service -> Repository -> DbContext -> SQL Server
```

Используются DTO, Dependency Injection и Entity Framework Core Migrations.

### Фичи

* CRUD для категорий
* CRUD для транзакций
* Фильтрация транзакций по категории и датам
* Валидация входных данных
* Связь `Category → Transactions`
* Обработка ошибок

### Запуск

Требуется .NET 8 SDK и Microsoft SQL Server.

```bash
dotnet ef database update
dotnet run
```

Swagger доступен после запуска приложения.
