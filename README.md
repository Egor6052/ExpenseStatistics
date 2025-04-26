# 📈 ExpenseStatistics

**ExpenseStatistics** — програма для швидкого аналізу витрат та фінансової статистики.

---
**_Content:_**
- [Task](#task-4)
- [Create app and build](#create-app-and-build)
- [Installing SDK and runtime machine](#installing-sdk-and-runtime-machine)

---
### Task 4:

API для отслеживания личных финансов.

**_Сущности:_** ТРАНЗАКЦИЯ, КАТЕГОРИЯ

**_Функционал:_**
Добавление категорий
Внесение доходов/расходов
Получение финансовой статистики (детализированной и сводной) за все время/за предыдущий месяц/за текущий месяц


_**Будет плюсом:**_ авторизация(транзакции привязываются к пользователю, получить можно только свою статистику), Статистика по категориям

---
### Create app and build
For creating app, use:

```sh
dotnet new console -n ExpenseStatistics
```
For build app, use:
```sh
dotnet build
```
For run, going to the build directory cd ./ExpenseStatistics, and:
```sh
dotnet run
```
---
#### Installing SDK and runtime machine
MacOS installing SDK:
```sh
brew install --cask dotnet-sdk@9
```
Fedora Linux installing SDK:
```sh
sudo dnf install dotnet-sdk-9.0
```
Install the runtime
```sh
sudo dnf install aspnetcore-runtime-9.0
```
and
```sh
sudo dnf install dotnet-runtime-9.0
```