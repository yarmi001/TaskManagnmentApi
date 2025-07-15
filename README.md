# Task Management API

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)
![Лицензия](https://img.shields.io/badge/license-MIT-green)
![Сборка](https://img.shields.io/badge/build-passing-brightgreen)

Task Management API — это RESTful API, разработанный на ASP.NET Core 8.0 для управления задачами с использованием JWT-аутентификации. API позволяет пользователям регистрироваться, входить в систему и выполнять CRUD-операции с задачами, при этом задачи каждого пользователя изолированы. Для упрощения используется In-Memory база данных (Entity Framework Core), а Swagger обеспечивает интерактивную документацию и тестирование.

## Возможности

- **Аутентификация пользователей**: Регистрация и вход с использованием JWT-токенов.
- **Управление задачами**: Создание, чтение, обновление и удаление задач.
- **Изоляция данных**: Задачи привязаны к аутентифицированному пользователю.
- **Swagger UI**: Интерактивная документация и тестирование API.
- **Безопасное хранение паролей**: Пароли хешируются с помощью BCrypt.
- **Чистая архитектура**: Разделение на модели, DTO, сервисы и контроллеры.

## Предварительные требования

- .NET 8.0 SDK
- IDE: Visual Studio 2022, Visual Studio Code или JetBrains Rider
- Git (для клонирования репозитория)
- Опционально: Postman или cURL для ручного тестирования API

## Установка

1. **Клонируйте репозиторий**:

   ```bash
   git clone https://github.com/your-username/TaskManagementApi.git
   cd TaskManagementApi
   ```

2. **Восстановите зависимости**:

   ```bash
   dotnet restore
   ```

3. **Соберите проект**:

   ```bash
   dotnet build
   ```

4. **Запустите проект**:

   ```bash
   dotnet run
   ```

   API запустится на `https://localhost:5001` (или другом порту, проверьте вывод в консоли).

## Использование

### Доступ к Swagger UI

API включает Swagger UI для интерактивного тестирования. Откройте браузер и перейдите по адресу:

```
https://localhost:<port>/swagger
```

Пример: `https://localhost:5001/swagger`

### Эндпоинты API

#### Аутентификация

- **POST /api/Auth/register**

  - Запрос:

    ```json
    {
      "username": "testuser",
      "password": "password123"
    }
    ```
  - Ответ: `200 OK`

    ```json
    {
      "message": "Пользователь успешно зарегистрирован"
    }
    ```

- **POST /api/Auth/login**

  - Запрос:

    ```json
    {
      "username": "testuser",
      "password": "password123"
    }
    ```
  - Ответ: `200 OK`

    ```json
    {
      "token": "<JWT_токен>"
    }
    ```

#### Задачи (Требуется JWT-аутентификация)

- **GET /api/Tasks**: Получить все задачи аутентифицированного пользователя.

  - Ответ: `200 OK`

    ```json
    [
      {
        "id": 1,
        "title": "Моя задача",
        "description": "Тестовая задача",
        "isCompleted": false,
        "createdAt": "2025-07-14T15:44:00Z",
        "dueDate": "2025-07-20T00:00:00Z",
        "userId": "testuser"
      }
    ]
    ```

- **GET /api/Tasks/{id}**: Получить задачу по ID.

  - Ответ: `200 OK` или `404 Not Found`

- **POST /api/Tasks**: Создать новую задачу.

  - Запрос:

    ```json
    {
      "title": "Моя задача",
      "description": "Тестовая задача",
      "dueDate": "2025-07-20T00:00:00Z"
    }
    ```
  - Ответ: `201 Created`

- **PUT /api/Tasks/{id}**: Обновить существующую задачу.

  - Запрос:

    ```json
    {
      "title": "Обновленная задача",
      "description": "Обновленное описание",
      "isCompleted": true,
      "dueDate": "2025-07-25T00:00:00Z"
    }
    ```
  - Ответ: `204 No Content`

- **DELETE /api/Tasks/{id}**: Удалить задачу.

  - Ответ: `204 No Content`

### Тестирование с помощью Swagger

1. Зарегистрируйте пользователя через `POST /api/Auth/register`.
2. Выполните вход через `POST /api/Auth/login`, чтобы получить JWT-токен.
3. Нажмите "Authorize" в Swagger UI (вверху справа) и введите `Bearer <токен>`.
4. Тестируйте защищенные эндпоинты, например, `POST /api/Tasks`.

### Тестирование с помощью Postman

1. Отправьте POST-запрос на `https://localhost:<port>/api/Auth/register`:

   ```json
   {
     "username": "testuser",
     "password": "password123"
   }
   ```
2. Отправьте POST-запрос на `https://localhost:<port>/api/Auth/login`, чтобы получить токен.
3. Добавьте токен в заголовок `Authorization`: `Bearer <токен>`.
4. Тестируйте эндпоинты задач (например, `POST /api/Tasks`).

### Тестирование с помощью cURL

```bash
# Регистрация
curl -X POST https://localhost:5001/api/Auth/register -H "Content-Type: application/json" -d '{"username":"testuser","password":"password123"}'

# Вход
curl -X POST https://localhost:5001/api/Auth/login -H "Content-Type: application/json" -d '{"username":"testuser","password":"password123"}'

# Создание задачи
curl -X POST https://localhost:5001/api/Tasks -H "Content-Type: application/json" -H "Authorization: Bearer <токен>" -d '{"title":"Тестовая задача","description":"Тест","dueDate":"2025-07-20T00:00:00Z"}'
```

## Структура проекта

```
TaskManagementApi/
├── Controllers/
│   ├── AuthController.cs      # Обработка аутентификации (регистрация/вход)
│   └── TasksController.cs     # Обработка CRUD-операций с задачами
├── Models/
│   ├── TaskItem.cs            # Модель задачи
│   └── User.cs                # Модель пользователя
├── Dtos/
│   ├── TaskCreateDto.cs       # DTO для создания задач
│   ├── TaskUpdateDto.cs       # DTO для обновления задач
│   └── UserLoginDto.cs        # DTO для регистрации/входа
├── Services/
│   ├── ITaskService.cs        # Интерфейс сервиса задач
│   ├── TaskService.cs         # Реализация сервиса задач
│   ├── IAuthService.cs        # Интерфейс сервиса аутентификации
│   └── AuthService.cs         # Реализация сервиса аутентификации
├── Data/
│   └── AppDbContext.cs        # Контекст Entity Framework Core
├── Program.cs                 # Точка входа приложения
├── appsettings.json           # Конфигурация (настройки JWT)
└── README.md                  # Документация проекта
```

## Зависимости

- `Microsoft.EntityFrameworkCore.InMemory` (8.0.8): In-Memory база данных для разработки.
- `Microsoft.AspNetCore.Authentication.JwtBearer` (8.0.8): JWT-аутентификация.
- `Swashbuckle.AspNetCore` (6.7.3): Swagger для документации API.
- `System.IdentityModel.Tokens.Jwt` (7.0.3): Обработка JWT-токенов.
- `BCrypt.Net-Next` (4.0.3): Хеширование паролей.

## Конфигурация

Файл `appsettings.json` содержит настройки JWT. Для продакшена храните `Jwt:Key` в переменных окружения:

```json
{
  "Jwt": {
    "Key": "YourSuperSecretKey1234567890",
    "Issuer": "TaskManagementApi",
    "Audience": "TaskManagementApiUsers"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

## Замечания по безопасности

- **Ключ JWT**: Замените `YourSuperSecretKey1234567890` на безопасный ключ в продакшене.
- **In-Memory база данных**: Используется для простоты. Для продакшена рассмотрите SQL Server или PostgreSQL.
- **Хеширование паролей**: Пароли безопасно хешируются с помощью BCrypt.

## Спецификация API

Спецификация OpenAPI доступна по адресу `https://localhost:<port>/swagger/v1/swagger.json`. Ее можно скачать для использования в инструментах, таких как Swagger Editor, или для генерации клиентского кода.


## Лицензия

Проект распространяется под лицензией MIT. См. файл LICENSE для подробностей.

## Контакты

- Автор: Ярослав
- Email: yarmisikov@gmail.com
- GitHub: yarmi001
