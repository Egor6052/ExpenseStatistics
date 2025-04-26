# 📈 ExpenseStatistics

**ExpenseStatistics** — програма для швидкого аналізу витрат та фінансової статистики.

---
**_Content:_**
- [Task](#task-4)
- [General requirements](#general-requirements)
- [Create app and build](#create-app-and-build)
- [Installing SDK and runtime machine](#installing-sdk-and-runtime-machine)
- [Managing PostgreSQL](#managing-postgresql)
- [API](#api)

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
### General requirements
https://docs.google.com/document/d/1z8VsEIK7fHzdaFKA2aUiP9kfheydpGkaO0p1NJZBc-Q/edit?tab=t.0
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
Or run from the environment:
```sh
ASPNETCORE_ENVIRONMENT=Development dotnet run
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

---
### Managing PostgreSQL

Installing the PostgreSQL:
```sh
brew install postgresql@16
```
So that the terminal can find the psql command (PostgreSQL client) without specifying the full path to it.
```sh
echo 'export PATH="/usr/local/opt/postgresql@16/bin:$PATH"' >> ~/.zshrc
source ~/.zshrc
```
Adding to the system services:
```sh
brew services start postgresql@16
```


### API 
```sh
http://localhost:5000/swagger/index.html
```