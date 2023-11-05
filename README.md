# Документация для проекта "Get_Info_by_INN_bot"

## Описание
Проект "Get_Info_by_INN_bot" представляет собой Telegram-бота, который позволяет пользователю получать информацию о компаниях по их ИНН. Проект включает в себя следующие основные компоненты:

- Telegram-бот для взаимодействия с пользователем.
- Модуль для извлечения информации о компаниях из внешнего API.
- Модуль для проверки файлов и конфигурации.

## Используемые технологии
Проект написан на языке C# и использует следующие библиотеки и инструменты:

- Microsoft.Extensions.Configuration для работы с конфигурацией.
- Telegram.Bot для взаимодействия с Telegram API.
- Newtonsoft.Json для разбора JSON-ответов от внешнего API.

## Основные компоненты

### BotContext
Класс `BotContext` представляет собой основную логику бота. Он отвечает за обработку сообщений от пользователей и реализует следующие команды:

- `/start` - начало общения с ботом.
- `/help` - вывод справки о доступных командах.
- `/inn` - команда для поиска информации о компании по ИНН.
- `/hello` - вывод информации о разработчике и дате.

### Extractor
Класс `Extractor` отвечает за взаимодействие с внешним API для извлечения информации о компаниях. Он включает в себя следующие методы:

- `GetTheFormatSearchResult(string rawContent)` - форматирует и анализирует результаты поиска и возвращает их в виде текстового сообщения.
- `GetCompanyInfoByINN(string inn, string api)` - отправляет запрос к внешнему API для поиска информации по ИНН и возвращает отформатированный результат.

### WorkFileChecker
Класс `WorkFileChecker` отвечает за проверку наличия файлов и создание их при необходимости. Этот компонент не связан напрямую с функциональностью бота, но важен для его корректной работы.

## Конфигурация
Конфигурация проекта осуществляется через файл `appsettings.json`. В этом файле хранятся токены и ключи API, необходимые для работы бота.

## Запуск проекта
Для запуска проекта необходимо выполнить следующие шаги:

1. Убедитесь, что файл `appsettings.json` содержит корректные токены и ключи API.
2. Запустите проект. Бот начнет работу и будет готов взаимодействовать с пользователями через Telegram.

